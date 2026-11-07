namespace Pinax.Domain.Common
{
	public abstract class BaseEntity
	{
		public int Id { get; private set; }
		public string OpInsert { get; set; }
		public DateTime DtInsert { get; set; }
		public string? OpUpdate { get; set; }
		public DateTime? DtUpdate { get; set; }
		public string? OpDelete { get; set; }
		public DateTime? DtDelete { get; set; }
		public bool Deleted { get; set; }
	}
}
