namespace Pinax.Application.Progetti
{
	public interface IProgettoService //contratto usato dal controller
	{
		Task<IReadOnlyList<ProgettoDto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default); //restituisce DTO, non entità
	}
}