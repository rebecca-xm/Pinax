using Pinax.Domain.Common;
using Pinax.Domain.Enums;

namespace Pinax.Domain.Entities
{
	public class Attivita : BaseEntity
	{
		public string Nome { get; set; } = string.Empty;
		public string? Descrizione { get; set; }
		public int ProgettoId { get; set; }
		public Progetto Progetto { get; set; } = null!;
		public int StatoId { get; set; }
		public Stato Stato { get; set; } = null!;
		public int Ordine { get; set; }
		public ICollection<AssegnazioneAttivita> Assegnazioni { get; set; } = new List<AssegnazioneAttivita>();
		public DateTime? Scadenza { get; set; }
		public Priorita Priorita { get; set; }
		public int Numero { get; set; }
	}
}
