using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pinax.Domain.Common;

namespace Pinax.Infrastructure.Persistence.Configurations
{
	public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> //classe generica: T è un segnaposto per il tipo di entità (es. Progetto)
		where T : BaseEntity //vincolo: T può essere solo una classe che eredita da BaseEntity, così si può usare i suoi campi
	{
		public void Configure(EntityTypeBuilder<T> builder) //metodo richiesto dall'interfaccia, configura la parte comune a tutte le entità
		{
			builder.HasKey(e => e.Id); //chiave primaria, uguale per tutte le entità

			builder.Property(e => e.OpInsert) //operatore che ha creato il record
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(e => e.OpUpdate) //operatore dell'ultima modifica, null finché il record non viene modificato
				.HasMaxLength(100);

			builder.Property(e => e.OpDelete) //operatore che ha cancellato il record, null finché il record non viene cancellato
				.HasMaxLength(100);

			builder.HasQueryFilter(e => !e.Deleted); //filtro globale: ogni query esclude in automatico i record con soft delete

			ConfigureEntity(builder); //passa il builder alla configurazione figlia per le regole specifiche dell'entità
		}

		protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder); //metodo astratto: ogni configurazione figlia è obbligata a scriverlo (pattern Template Method)
	}
}