using Eventos.Domain.Identity;

namespace Eventos.Domain;

public class Palestrante
{
	public int Id { get; set; }
	public string MiniCurriculo { get; set; }
	public int UsuarioId { get; set; }
	public Usuario Usuario { get; set; }
	
	public IEnumerable<RedeSocial> RedeSociais { get; set; }
	public IEnumerable<PalestranteEvento> PalestranteEventos { get; set; }
}