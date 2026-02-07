using Eventos.Application.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;

namespace Eventos.Application.Contratos;

public interface IContaService
{
	Task<bool> UsuarioExiste(string userName);
	Task<UpdateUsuarioDto?> GetUsuarioByUserName(string userName);
	Task<SignInResult> VerificarSenhaUsuarioAsync(UpdateUsuarioDto updateUsuarioDto, string senha);
	Task<UsuarioDto?> CriarContaAsync(UsuarioDto usuarioDto);
	Task<UpdateUsuarioDto?> UpdateConta(UpdateUsuarioDto updateUsuarioDto);
}