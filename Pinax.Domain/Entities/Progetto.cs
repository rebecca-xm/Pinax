using Pinax.Domain.Common;

namespace Pinax.Domain.Entities
{
	public class Progetto : BaseEntity
	{
		public string Nome { get; set; }
		public string Codice { get; set; }
		public string? Descrizione { get; set; }
		public string? Colore { get; set; }
		public bool Archiviato { get; set; }
		public ICollection<Stato> Stati { get; set; } = new List<Stato>();
		public ICollection<Attivita> Attivita { get; set; } = new List<Attivita>();
	}
}
