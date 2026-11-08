namespace Pinax.Application.Progetti
{
	public class ProgettoService : IProgettoService
	{
		private readonly IProgettoRepository _repository; //dipende dall'interfaccia, non dall'implementazione

		public ProgettoService(IProgettoRepository repository) //il repository arriva dalla dependency injection
		{
			_repository = repository;
		}

		public async Task<IReadOnlyList<ProgettoDto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default)
		{
			var progetti = await _repository.GetAllAsync(includiArchiviati, cancellationToken); //aspetta il risultato senza bloccare il thread

			return progetti
				.Select(p => new ProgettoDto(p.Id, p.Codice, p.Nome, p.Descrizione, p.Colore, p.Archiviato)) //trasforma ogni entità in DTO
				.ToList();
		}
	}
}