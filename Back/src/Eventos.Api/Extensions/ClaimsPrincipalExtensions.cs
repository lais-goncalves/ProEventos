using System.Security.Claims;

namespace Eventos.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static string? GetUserName(this ClaimsPrincipal usuario)
	{
		return usuario.FindFirst(ClaimTypes.Name)?.Value;
	}
	
	public static int GetId(this ClaimsPrincipal usuario)
	{
		return int.Parse(usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
	}
}