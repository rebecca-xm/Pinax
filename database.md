# Pinax - appunti su database e migration

I comandi si lanciano dalla Console di Gestione pacchetti
(Strumenti > Gestione pacchetti NuGet > Console di Gestione pacchetti).

Prima di qualsiasi comando controllare che:
- il progetto di avvio sia `Pinax.API` (quello in grassetto in Esplora soluzioni)
- nella console, come progetto predefinito, ci sia `Pinax.Infrastructure`
- la soluzione compili

La stringa di connessione sta in `Pinax.API/appsettings.json`, sotto `ConnectionStrings:PinaxDb`.


## Database da zero (es. dopo un clone)

```powershell
Update-Database
```

Crea il db Pinax e applica tutte le migration in ordine.


## Quando si cambia qualcosa

Si modificano le entità (`Domain/Entities`) o le configurazioni (`Infrastructure/Persistence/Configurations`), si compila, poi:

```powershell
Add-Migration NomeMigration -OutputDir Persistence/Migrations
```

Il nome deve dire cosa cambia, tipo `AddEtichette` o `AddScadenzaToAttivita`.

Prima di applicarla aprire il file generato e leggere il metodo `Up`: lunghezze delle stringhe, nullable, Restrict sulle FK, indici e filtri su Deleted. Se si vedono `DropColumn` o `DropTable` ci si ferma, perché si perdono dati.

Se è tutto ok:

```powershell
Update-Database
```

Si controlla in SSMS e si fa il commit, cartella `Migrations` compresa.


## Se una migration è sbagliata

Non ancora applicata: `Remove-Migration`, si corregge il codice e la si rigenera.

Già applicata: non si tocca. Si corregge il codice e se ne crea una nuova che sistema il problema.

Applicata solo in locale, da annullare:

```powershell
Update-Database NomeMigrationPrecedente
Remove-Migration
```


## Altri comandi utili

```powershell
Get-Migration                                   # elenco migration e quali sono applicate
Update-Database NomeMigration                   # torna a una migration precedente
Update-Database 0                               # annulla tutto, db vuoto
Script-Migration -Idempotent -Output pinax.sql  # genera lo script SQL invece di applicarlo
Drop-Database                                   # cancella il db (solo in locale!)
```


## Da terminale, senza Visual Studio

Una volta sola:

```bash
dotnet tool install --global dotnet-ef
```

Poi dalla cartella della soluzione:

```bash
dotnet ef migrations add NomeMigration --project Pinax.Infrastructure --startup-project Pinax.API --output-dir Persistence/Migrations
dotnet ef database update --project Pinax.Infrastructure --startup-project Pinax.API
dotnet ef migrations remove --project Pinax.Infrastructure --startup-project Pinax.API
```


## Da ricordare

- Una migration già applicata non si modifica mai.
- Snapshot e file `.Designer.cs` non si toccano a mano.
- I pacchetti EF Core devono avere la stessa versione in tutti i progetti.
- Finché il db esiste solo in locale si possono eliminare tutte le migration e rifare una `InitialCreate` pulita.


## Errori frequenti

- **Build failed**: la soluzione non compila.
- **Unable to create a DbContext**: il progetto di avvio non è Pinax.API, oppure manca `AddInfrastructure` in `Program.cs`.
- **No DbContext was found**: nella console il progetto predefinito non è Infrastructure.
- **Errore sul certificato**: manca `TrustServerCertificate=True` nella stringa di connessione.
- **Multiple cascade paths**: manca `OnDelete(DeleteBehavior.Restrict)` su qualche relazione.
- **Add-Migration non riconosciuto**: manca `Microsoft.EntityFrameworkCore.Tools` in Pinax.API.