using Microsoft.EntityFrameworkCore;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence
{
	public class PinaxDbContext : DbContext
	{
		public PinaxDbContext(DbContextOptions<PinaxDbContext> options) : base(options) //contruttore
		{
		}

		public DbSet<Progetto> Progetti => Set<Progetto>(); //tabella entità
		public DbSet<Attivita> Attivita => Set<Attivita>();
		public DbSet<Stato> Stati => Set<Stato>();
		public DbSet<AssegnazioneAttivita> AssegnazioniAttivita => Set<AssegnazioneAttivita>();
		public DbSet<Utente> Utenti => Set<Utente>();

		protected override void OnModelCreating(ModelBuilder modelBuilder) //metodo chiamato da EF quando costruisce il modello del database
		{
			base.OnModelCreating(modelBuilder); //esegue prima la configurazione di base di DbContext

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PinaxDbContext).Assembly); //cerca e applica tutte le classi di configurazione presenti in questo progetto
		}
	}
}
