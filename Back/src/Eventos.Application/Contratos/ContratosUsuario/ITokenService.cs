using Eventos.Application.Dtos.Usuario;

namespace Eventos.Application.Contratos;

public interface ITokenService
{
	Task<string> CreateToken(UpdateUsuarioDto updateUsuarioDto);
}