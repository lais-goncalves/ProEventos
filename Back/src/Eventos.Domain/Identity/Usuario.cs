using Eventos.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Eventos.Domain.Identity;

public class Usuario: IdentityUser<int>
{
	public string PrimeiroNome { get; set; }
	public string UltimoNome { get; set; }
	public Titulo? Titulo { get; set; }
	public string? Descricao { get; set; }
	public Funcao? Funcao { get; set; }
	public string? ImagemPerfilURL { get; set; }
	public List<PapelUsuario>? PapeisUsuario { get; set; }
}