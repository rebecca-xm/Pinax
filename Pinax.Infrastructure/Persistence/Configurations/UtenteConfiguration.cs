using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Entities;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public class UtenteConfiguration : BaseEntityConfiguration<Utente> //eredita la configurazione comune da BaseEntityConfiguration
	{
		protected override void ConfigureEntity(EntityTypeBuilder<Utente> builder)
		{
			builder.ToTable("Utenti"); //nome tabella

			builder.Property(u => u.Nome) //proprietà Nome
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(u => u.Cognome) //proprietà Cognome
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(u => u.Username) //proprietà Username
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(u => u.Email) //proprietà Email
				.IsRequired()
				.HasMaxLength(254);

			builder.Property(u => u.PasswordHash) //proprietà PasswordHash
				.IsRequired()
				.HasMaxLength(256);

			builder.HasIndex(u => u.Username)
				.IsUnique(); //indice univoco per Username

			builder.HasIndex(u => u.Email)
				.IsUnique(); //indice univoco per Email
		}
	}
}
