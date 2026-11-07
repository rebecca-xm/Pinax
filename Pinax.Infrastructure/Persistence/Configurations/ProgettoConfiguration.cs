using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public class ProgettoConfiguration : IEntityTypeConfiguration<Progetto> //implementazione interfaccia
	{
		public void Configure(EntityTypeBuilder<Progetto> builder)
		{
			builder.ToTable("Progetti"); //nome tabella

			builder.HasKey(p => p.Id); //chiave primaria

			builder.Property(p => p.Nome) //proprietà Nome
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Codice) //proprietà Codice
				.IsRequired()
				.HasMaxLength(10);

			builder.HasIndex(p => p.Codice)
				.IsUnique()
				.HasFilter("[Deleted] = 0"); // indice univoco solo per i record non eliminati

			builder.Property(p => p.Descrizione)
				.HasMaxLength(1000);

			builder.Property(p => p.Colore)
				.HasMaxLength(7);
		}
	}
}
