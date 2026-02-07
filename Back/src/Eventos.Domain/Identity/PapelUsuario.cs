using Microsoft.AspNetCore.Identity;

namespace Eventos.Domain.Identity;

public class PapelUsuario : IdentityUserRole<int>
{
	public Usuario Usuario { get; set; }
	public Papel Papel { get; set; }
}