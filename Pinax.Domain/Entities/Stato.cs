using Pinax.Domain.Common;

namespace Pinax.Domain.Entities
{
	public class Stato : BaseEntity
	{
		public string Nome { get; set; }
		public int Ordine { get; set; }
		public int ProgettoId { get; set; }
		public Progetto Progetto { get; set; } = null!;
	}
}
