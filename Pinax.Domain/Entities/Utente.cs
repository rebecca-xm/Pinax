using Pinax.Domain.Common;

namespace Pinax.Domain.Entities
{
	public class Utente : BaseEntity
	{
		public string Nome { get; set; } = string.Empty;
		public string Cognome { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public ICollection<AssegnazioneAttivita> Assegnazioni { get; set; } = new List<AssegnazioneAttivita>();
	}
}
