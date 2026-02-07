using Eventos.Domain.Identity;

namespace Eventos.Persistence.Contratos;

public interface IUsuarioPersist : IGeralPersist
{
	Task<IEnumerable<Usuario>> GetUsuariosAsync();
	Task<Usuario?> GetUsuarioByIdAsync(int id);
	Task<Usuario?> GetUsuarioByUserNameAsync(string userName);
}