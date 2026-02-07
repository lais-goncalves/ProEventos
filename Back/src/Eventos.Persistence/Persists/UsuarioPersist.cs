using Eventos.Domain.Identity;
using Eventos.Persistence.Contratos;
using Eventos.Persistence.Persists;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence;

public class UsuarioPersist(EventosContext context) : GeralPersist(context), IUsuarioPersist
{
	private EventosContext _context = context;

	public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
	{
		return await _context.Users.ToListAsync();
	}
	
	public async Task<Usuario?> GetUsuarioByIdAsync(int id)
	{
		Usuario? usuario = await _context.Users.FindAsync(id);
		return usuario;
	}
	
	public async Task<Usuario?> GetUsuarioByUserNameAsync(string userName)
	{
		Usuario? usuario = await _context.Users
		                                 .SingleOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
		return usuario;
	}

	private string ConverterParaLowerCase(string valor)
	{
		return valor.Trim().ToLower();
	}
}