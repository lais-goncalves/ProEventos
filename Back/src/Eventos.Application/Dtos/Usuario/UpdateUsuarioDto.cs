using Microsoft.AspNetCore.Identity;

namespace Eventos.Application.Dtos.Usuario;

public class UpdateUsuarioDto
{
	public int Id { get; set; }
	public string? Titulo { get; set; }
	public string UserName { get; set; }
	public string PrimeiroNome { get; set; }
	public string UltimoNome { get; set; }
	public string? Email { get; set; }
	public string? Telefone { get; set; }
	public string? Funcao { get; set; }
	public string? Descricao { get; set; }
	public string Senha { get; set; }
	public string Token { get; set; }
}