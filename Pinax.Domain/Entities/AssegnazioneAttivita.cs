using Pinax.Domain.Common;

namespace Pinax.Domain.Entities
{
	public class AssegnazioneAttivita: BaseEntity
	{
		public int AttivitaId { get; set; }
		public Attivita Attivita { get; set; } = null!;
		public int UtenteId { get; set; }
		public Utente Utente { get; set; } = null!;
	}
}
