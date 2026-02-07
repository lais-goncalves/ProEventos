using AutoMapper;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos.Usuario;
using Eventos.Domain.Identity;
using Eventos.Persistence.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Eventos.Domain.Identity.Usuario;

namespace Eventos.Application.ServicesUsuario;

public class ContaService : IContaService
{
	private readonly UserManager<Usuario> _usuarioManager;
	private readonly SignInManager<Usuario> _signInManager;
	private readonly IMapper _mapper;
	private readonly IUsuarioPersist _usuarioPersist;

	public ContaService(
		UserManager<Usuario> usuarioManager, 
		SignInManager<Usuario> signInManager, 
		IMapper mapper,
		IUsuarioPersist usuarioPersist
		)
	{
		_usuarioManager = usuarioManager;
		_signInManager = signInManager;
		_mapper = mapper;
		_usuarioPersist = usuarioPersist;
	}
	
	public async Task<bool> UsuarioExiste(string userName)
	{
		try
		{
			Usuario? usuario = await _usuarioPersist.GetUsuarioByUserNameAsync(userName);
			return usuario != null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception($"Erro ao tentar verificar se usuário existe. Erro: {e.Message}");
		}
	}
	
	public async Task<UpdateUsuarioDto?> GetUsuarioByUserName(string userName)
	{
		try
		{
			Usuario? usuario = await _usuarioPersist.GetUsuarioByUserNameAsync(userName);
			if (usuario == null) return null;
			
			UpdateUsuarioDto updateUsuarioDto = _mapper.Map<UpdateUsuarioDto>(usuario);
			return updateUsuarioDto;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception($"Erro ao tentar buscar usuário. Erro: {e.Message}");
		}
	}
	
	public async Task<SignInResult> VerificarSenhaUsuarioAsync(UpdateUsuarioDto updateUsuarioDto, string senha)
	{
		try
		{
			Usuario usuario = await _usuarioManager
			                        .Users
			                        .SingleOrDefaultAsync(u => u.UserName.ToLower() ==  updateUsuarioDto.UserName.ToLower());
			
			return await _signInManager.CheckPasswordSignInAsync(usuario, senha, false);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception($"Erro ao tentar verificar senha. Erro: {e.Message}");
		}
	}
	
	public async Task<UsuarioDto?> CriarContaAsync(UsuarioDto usuarioDto)
	{
		try
		{
			Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
			var resultado = await _usuarioManager.CreateAsync(usuario, usuarioDto.Senha);

			if (resultado.Succeeded)
			{
				var usuarioResultadoDto = _mapper.Map<UsuarioDto>(usuario);
				return  usuarioResultadoDto;
			}

			return null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception($"Erro ao tentar criar conta. Erro: {e.Message}");
		}
	}
	
	public async Task<UpdateUsuarioDto?> UpdateConta(UpdateUsuarioDto updateUsuarioDto)
	{
		try
		{
			Usuario? usuario = await _usuarioPersist.GetUsuarioByUserNameAsync(updateUsuarioDto.UserName);
			if (usuario is null) return null;

			_mapper.Map(updateUsuarioDto, usuario);

			var token = await _usuarioManager.GeneratePasswordResetTokenAsync(usuario);
			var resultado = await _usuarioManager.ResetPasswordAsync(usuario, token, updateUsuarioDto.Senha);
			
			_usuarioPersist.Update(usuario);

			if (!await _usuarioPersist.SaveChangesAsync()) return null;
			
			var usuarioResultadoDto = _mapper.Map<UpdateUsuarioDto>(usuario);
			return usuarioResultadoDto;

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception($"Erro ao tentar atualizar conta. Erro: {e.Message}");
		}
	}
}