using System.Text.Json.Serialization;
using Eventos.Domain.Identity;

namespace Eventos.Domain;

public class Evento
{
	public int Id { get; set; }
	public string Local { get; set; }
	public DateTime? DataEvento { get; set; }
	public string Tema { get; set; }
	public int QtdPessoas { get; set; }
	public string ImagemURL { get; set; }
	public string? Email { get; set; }
	public string? Telefone { get; set; }
	public int UsuarioId { get; set; }
	public Usuario Usuario { get; set; }
	
	public IEnumerable<Lote>? Lotes { get; set; }
	public IEnumerable<RedeSocial>? RedesSociais { get; set; }
	public IEnumerable<PalestranteEvento>? PalestrantesEventos { get; set; }
}