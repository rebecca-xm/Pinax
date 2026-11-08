# Come si costruisce una funzionalità in Application

Appunti su come è organizzato il progetto Application e su come si aggiunge una funzionalità nuova, usando come esempio la lista dei progetti.


## Dove sta Application nella Clean Architecture

```
Pinax.API             controller, Program.cs
   │
   ▼
Pinax.Application     DTO, interfacce, servizi (logica dell'applicazione)
   │
   ▼
Pinax.Domain          entità, enum (le regole del dominio)

Pinax.Infrastructure  DbContext, configurazioni, migration, repository
   └── dipende da Application e ne implementa le interfacce
```

Le dipendenze puntano verso il centro. Application conosce il Domain, ma non sa niente di EF Core, di SQL Server o di HTTP. Quando le serve qualcosa dal database lo chiede attraverso un'interfaccia, e l'implementazione la scrive Infrastructure.


## Cartelle per funzionalità e cartelle per tipo

Ci sono due modi di organizzare i file.

**Per tipo**, come si fa spesso nei progetti a strati:

```
Pinax.Application
├── Dtos
│   ├── ProgettoDto.cs
│   └── AttivitaDto.cs
├── Interfaces
│   ├── IProgettoRepository.cs
│   ├── IProgettoService.cs
│   ├── IAttivitaRepository.cs
│   └── IAttivitaService.cs
└── Services
    ├── ProgettoService.cs
    └── AttivitaService.cs
```

**Per funzionalità**, che è la scelta fatta in Pinax:

```
Pinax.Application
├── Progetti
│   ├── ProgettoDto.cs
│   ├── IProgettoRepository.cs
│   ├── IProgettoService.cs
│   └── ProgettoService.cs
└── Attivita
    ├── AttivitaDto.cs
    ├── IAttivitaRepository.cs
    ├── IAttivitaService.cs
    └── AttivitaService.cs
```

Con le cartelle per tipo, per lavorare su una sola funzionalità si aprono tre o quattro cartelle diverse, e più il progetto cresce più ognuna diventa lunga. Con le cartelle per funzionalità tutto quello che riguarda i progetti sta in un posto solo: si apre una cartella e si ha il quadro completo. Aggiungere o togliere una funzionalità vuol dire aggiungere o togliere una cartella.

Le cartelle per tipo hanno il vantaggio di essere familiari e di funzionare bene finché il progetto è piccolo. Il problema si vede quando le funzionalità diventano tante.

Se una cartella di funzionalità si affolla (cinque o sei DTO, per esempio) si possono aggiungere sottocartelle. Ma solo quando serve.

Questa organizzazione vale per Application. Infrastructure resta divisa per tipo tecnico (`Persistence`, `Configurations`, `Migrations`, `Repositories`), perché lì i file sono raggruppati per tecnologia.


## I file di una funzionalità, in ordine

### 1. Il DTO

È quello che esce dall'API. Si scrive come `record`:

```csharp
namespace Pinax.Application.Progetti
{
	public record ProgettoDto(
		int Id,
		string Codice,
		string Nome,
		string? Descrizione,
		string? Colore,
		bool Archiviato);
}
```

Un record è pensato per trasportare dati: in una riga si dichiarano le proprietà, e C# genera costruttore e proprietà in sola lettura. Due record con gli stessi valori risultano uguali.

Nel DTO ci sono solo i campi che servono al client. I campi di audit (`OpInsert`, `DtInsert`, `Deleted`...) restano nell'entità.

**Un DTO per la lettura, uno per la scrittura.** Un DTO unico per tutto è più semplice da mantenere, ma mescola esigenze diverse: in lettura l'Id serve, in scrittura no, perché lo genera il database. Con un DTO unico il backend deve ricordarsi di ignorare i campi che il client non dovrebbe mandare, e se se ne dimentica qualcuno può sovrascriverli (si chiama *overposting*). In Pinax:

- `ProgettoDto` per la lettura
- `ProgettoSaveDto` per creazione e modifica, senza Id: nella modifica l'Id arriva dall'URL (`PUT /api/progetti/5`)

Si separano ulteriormente in `CreateDto` e `UpdateDto` solo se i campi modificabili sono diversi da quelli di creazione.


### 2. L'interfaccia del repository

```csharp
using Pinax.Domain.Entities;

namespace Pinax.Application.Progetti
{
	public interface IProgettoRepository
	{
		Task<IReadOnlyList<Progetto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default);
	}
}
```

Sta in Application, ma l'implementazione sta in Infrastructure. Application dice cosa le serve, Infrastructure decide come ottenerlo. Così il database diventa un dettaglio sostituibile, e nei test si può passare un repository finto che tiene i dati in memoria.

- `Task` e il suffisso `Async`: il metodo è asincrono, e mentre aspetta il database il thread può servire altre richieste.
- `IReadOnlyList`: chi riceve la lista può leggerla ma non modificarla.
- `CancellationToken`: se il client chiude la richiesta, la query viene annullata. Con `= default` il parametro è facoltativo.

Il repository restituisce **entità**, non DTO: lavora con il dominio.


### 3. L'interfaccia del servizio

```csharp
namespace Pinax.Application.Progetti
{
	public interface IProgettoService
	{
		Task<IReadOnlyList<ProgettoDto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default);
	}
}
```

È quella che usa il controller. Restituisce **DTO**, non entità: il controller non vede mai il dominio.


### 4. Il servizio

```csharp
namespace Pinax.Application.Progetti
{
	public class ProgettoService : IProgettoService
	{
		private readonly IProgettoRepository _repository;

		public ProgettoService(IProgettoRepository repository)
		{
			_repository = repository;
		}

		public async Task<IReadOnlyList<ProgettoDto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default)
		{
			var progetti = await _repository.GetAllAsync(includiArchiviati, cancellationToken);

			return progetti
				.Select(p => new ProgettoDto(p.Id, p.Codice, p.Nome, p.Descrizione, p.Colore, p.Archiviato))
				.ToList();
		}
	}
}
```

- Il repository arriva dal costruttore (**iniezione delle dipendenze**). Il servizio non lo crea con `new`: chi glielo passa lo decide la registrazione all'avvio.
- `readonly`: il campo si assegna solo nel costruttore.
- `async`/`await`: `await` aspetta il risultato senza bloccare il thread. Un metodo che usa `await` deve essere `async`.
- `Select` è l'equivalente del `map` di JavaScript: trasforma ogni entità in un DTO. `ToList()` esegue la trasformazione.

La mappatura entità → DTO si scrive a mano: per pochi campi è chiara e si vede subito cosa succede. Librerie come AutoMapper esistono, ma nascondono la logica e oggi molti progetti le evitano.

Il servizio è anche il posto per la logica dell'applicazione: controlli, regole che coinvolgono più entità, calcoli. Per una lista semplice fa solo la mappatura, ma per la creazione di un'attività, per esempio, calcolerà il numero progressivo nel progetto.


## Il resto del percorso

Application da sola non basta. Una funzionalità completa tocca anche:

1. **Infrastructure** - la classe che implementa il repository, in `Persistence/Repositories`, e che usa il `PinaxDbContext`.
2. **Registrazione** - servizio e repository vanno registrati nella dependency injection, ognuno nel suo progetto (`AddApplication` e `AddInfrastructure`), e chiamati da `Program.cs`.
3. **API** - il controller, che riceve `IProgettoService` nel costruttore ed espone l'endpoint.


## Checklist per una funzionalità nuova

Esempio: le attività.

- [ ] cartella `Attivita` in Pinax.Application
- [ ] `AttivitaDto` (lettura) ed eventualmente `AttivitaSaveDto` (scrittura)
- [ ] `IAttivitaRepository`, con metodi che restituiscono entità
- [ ] `IAttivitaService`, con metodi che restituiscono DTO
- [ ] `AttivitaService`, con il repository iniettato nel costruttore
- [ ] `AttivitaRepository` in Infrastructure
- [ ] registrazione di servizio e repository nella dependency injection
- [ ] `AttivitaController` nell'API
- [ ] compilazione e prova dell'endpoint
- [ ] commit sul ramo `feature/...` e pull request