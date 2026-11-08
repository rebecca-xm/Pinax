namespace Pinax.Application.Progetti
{
	public record ProgettoDto( //record: tipo di dato immutabile, utile per trasferire dati tra livelli dell'applicazione
		int Id,
		string Codice,
		string Nome,
		string? Descrizione,
		string? Colore,
		bool Archiviato);
}
