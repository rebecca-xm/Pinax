using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pinax.Application.Progetti;
using Pinax.Infrastructure.Persistence;
using Pinax.Infrastructure.Persistence.Repositories;

namespace Pinax.Infrastructure
{
	public static class DependencyInjection //classe statica: contiene solo metodi di estensione, non si istanzia
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) //metodo di estensione: si aggiunge a IServiceCollection
		{
			services.AddDbContext<PinaxDbContext>(options => //registra il DbContext nella dependency injection
				options.UseSqlServer(configuration.GetConnectionString("PinaxDb"))); //usa SQL Server con la stringa di connessione letta da appsettings.json

			services.AddScoped<IProgettoRepository, ProgettoRepository>(); //registra il repository nella dependency injection: ogni richiesta ottiene una nuova istanza, condivisa tra i servizi che la richiedono

			return services; //restituisce la collezione per poter concatenare altre registrazioni
		}
	}
}