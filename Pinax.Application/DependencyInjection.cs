using Microsoft.Extensions.DependencyInjection;
using Pinax.Application.Progetti;

namespace Pinax.Application
{
	public static class DependencyInjection //registra i servizi dello strato Application
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IProgettoService, ProgettoService>(); //registra il servizio nella dependency injection: ogni richiesta ottiene una nuova istanza, condivisa tra i servizi che la richiedono

			return services;
		}
	}
}