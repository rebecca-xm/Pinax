using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public class AttivitaConfiguration : BaseEntityConfiguration<Attivita> //eredita la configurazione comune da BaseEntityConfiguration
	{
		protected override void ConfigureEntity(EntityTypeBuilder<Attivita> builder)
		{
			builder.ToTable("Attivita"); //nome tabella

			builder.Property(a => a.Nome) //proprietà Nome
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(a => a.Descrizione) //proprietà Descrizione
				.HasMaxLength(4000);

			builder.HasOne(a => a.Progetto) //l'Attivita ha un Progetto
				.WithMany(p => p.Attivita) //quel Progetto ha molte Attivita
				.HasForeignKey(a => a.ProgettoId) //il collegamento è la colonna ProgettoId dell'Attivita
				.OnDelete(DeleteBehavior.Restrict); //non si può cancellare un Progetto che ha ancora Attivita

			builder.HasOne(a => a.Stato) //l'Attivita ha uno Stato
				.WithMany(s => s.Attivita) //quello Stato ha molte Attivita
				.HasForeignKey(a => a.StatoId) //il collegamento è la colonna StatoId dell'Attivita
				.OnDelete(DeleteBehavior.Restrict); //non si può cancellare uno Stato che ha ancora Attivita

			//HasOne -> entità che si sta configurando
			//HasForeignKey -> entità che si sta configurando
			//WithMany -> entità correlata

			builder.HasIndex(a => new { a.StatoId, a.Ordine }); //indice per ottimizzare le query che filtrano per StatoId e Ordinamento

			builder.HasIndex(a => new { a.ProgettoId, a.Numero })
				.IsUnique(); //indice univoco per ProgettoId e Numero, in modo che non ci siano due Attivita con lo stesso Numero nello stesso Progetto
		}
	}
}
