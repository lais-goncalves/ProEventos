using Microsoft.AspNetCore.Identity;

namespace Eventos.Domain.Identity;

public class Papel : IdentityRole<int>
{
	public List<PapelUsuario> PapeisUsuario { get; set; }
}