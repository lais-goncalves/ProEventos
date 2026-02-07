using Microsoft.AspNetCore.Identity;

namespace Eventos.Application.Dtos.Usuario;

public class UsuarioDto
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Senha { get; set; }
	public string PrimeiroNome { get; set; }
	public string UltimoNome { get; set; }
}