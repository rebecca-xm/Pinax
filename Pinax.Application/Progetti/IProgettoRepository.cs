using Pinax.Domain.Entities;

namespace Pinax.Application.Progetti
{
	public interface IProgettoRepository //contratto: Application dice cosa le serve, Infrastructure decide come farlo
	{
		Task<IReadOnlyList<Progetto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default); //restituisce i progetti, con o senza quelli archiviati
	}
}
