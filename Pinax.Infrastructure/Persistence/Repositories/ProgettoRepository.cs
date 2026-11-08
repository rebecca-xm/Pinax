using Microsoft.EntityFrameworkCore;
using Pinax.Application.Progetti;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Repositories
{
	public class ProgettoRepository : IProgettoRepository //implementa il contratto definito in Application
	{
		private readonly PinaxDbContext _context; //il DbContext arriva dalla dependency injection

		public ProgettoRepository(PinaxDbContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<Progetto>> GetAllAsync(bool includiArchiviati, CancellationToken cancellationToken = default)
		{
			var query = _context.Progetti.AsNoTracking(); //sola lettura: EF non tiene traccia delle modifiche

			if (!includiArchiviati)
				query = query.Where(p => !p.Archiviato); //il filtro si aggiunge solo se serve

			query = query.OrderBy(p => p.Nome); //ordinamento alfabetico

			//System.Diagnostics.Debug.WriteLine(query.ToQueryString()); //DEBUG: scrive il SQL nella finestra Output

			return await query.ToListAsync(cancellationToken); //esegue la query in modo asincrono, senza bloccare il thread
		}
	}
}