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
	}
}
