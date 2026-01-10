using System.Text.Json.Serialization;

namespace Eventos.Domain;

public class PalestranteEvento
{
	public int PalestranteId { get; set; }
	[JsonIgnore]
	public Palestrante Palestrante { get; set; }
	public int EventoId { get; set; }
	[JsonIgnore]
	public Evento Evento { get; set; }
}