using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public class AssegnazioneAttivitaConfiguration : BaseEntityConfiguration<AssegnazioneAttivita> //eredita la configurazione comune da BaseEntityConfiguration
	{
		protected override void ConfigureEntity(EntityTypeBuilder<AssegnazioneAttivita> builder)
		{
			builder.ToTable("AssegnazioniAttivita"); //nome tabella

			builder.HasOne(a => a.Attivita) //l'AssegnazioneAttivita ha un'Attivita
				.WithMany(att => att.Assegnazioni) //quell'Attivita ha molte AssegnazioneAttivita
				.HasForeignKey(a => a.AttivitaId) //il collegamento è la colonna AttivitaId dell'AssegnazioneAttivita
				.OnDelete(DeleteBehavior.Restrict); //non si può cancellare un'Attivita che ha ancora AssegnazioneAttivita

			builder.HasOne(a => a.Utente) //l'AssegnazioneAttivita ha un Utente
				.WithMany(u => u.Assegnazioni) //quell'Utente ha molte AssegnazioneAttivita
				.HasForeignKey(a => a.UtenteId) //il collegamento è la colonna UtenteId dell'AssegnazioneAttivita
				.OnDelete(DeleteBehavior.Restrict); //non si può cancellare un Utente che ha ancora AssegnazioneAttivita

			builder.HasIndex(a => new { a.AttivitaId, a.UtenteId })
				.IsUnique()
				.HasFilter("[Deleted] = 0"); //una persona può essere assegnata una sola volta alla stessa attività, tra i record non eliminati
		}
	}
}
