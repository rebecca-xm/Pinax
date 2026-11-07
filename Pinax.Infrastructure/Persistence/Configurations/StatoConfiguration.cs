using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public class StatoConfiguration : BaseEntityConfiguration<Stato> //eredita la configurazione comune da BaseEntityConfiguration
	{
		protected override void ConfigureEntity(EntityTypeBuilder<Stato> builder)
		{
			builder.ToTable("Stati"); //nome tabella

			builder.Property(s => s.Nome) //proprietà Nome
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.Ordine) //proprietà Ordine
				.IsRequired();

			builder.HasOne(s => s.Progetto) //relazione uno a molti con Progetto
				.WithMany(p => p.Stati) //relazione uno a molti con Progetto
				.HasForeignKey(s => s.ProgettoId) //relazione uno a molti con Progetto
				.OnDelete(DeleteBehavior.Restrict); //impedisce la cancellazione di un Progetto se ci sono Stati associati

			builder.HasIndex(s => new { s.ProgettoId, s.Nome })
				.IsUnique()
				.HasFilter("[Deleted] = 0"); //indice univoco solo per i record non eliminati
		}
	}
}
