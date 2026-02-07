using System.Security.Claims;
using Eventos.Api.Extensions;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos.Usuario;
using Eventos.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AutorizarController : ControllerBase
{
	private readonly IContaService _contaService;
	private readonly ITokenService _tokenService;

	public AutorizarController(
		IContaService contaService,
		ITokenService tokenService
	)
	{
		_contaService = contaService;
		_tokenService = tokenService;
	}

	[HttpGet("GetUsuario")]
	public async Task<IActionResult> GetUsuario()
	{
		try
		{
			string? userName = User.GetUserName();
			UpdateUsuarioDto? usuario = await _contaService.GetUsuarioByUserName(userName);
			return Ok(usuario);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Erro ao tentar buscar usuário. Erro: {e.Message}");
		}
	}
	
	[HttpPost("RegisterUsuario")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterUsuario(UsuarioDto usuarioDto)
	{
		try
		{
			if (await _contaService.UsuarioExiste(usuarioDto.UserName))
			{
				return BadRequest($"Usuário {usuarioDto.UserName} já existe.");
			}

			UsuarioDto? usuario = await _contaService.CriarContaAsync(usuarioDto);
			if (usuario == null) return BadRequest("Usuário não criado. Tente novamente mais tarde.");

			return Ok(usuarioDto);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Erro ao tentar registrar usuário. Erro: {e.Message}");
		}
	}
	
	[HttpPost("LoginUsuario")]
	[AllowAnonymous]
	public async Task<IActionResult> LoginUsuario(UsuarioLoginDto usuarioLoginDto)
	{
		try
		{
			UpdateUsuarioDto? usuario = await _contaService.GetUsuarioByUserName(usuarioLoginDto.UserName);
			if (usuario == null) return Unauthorized("Usuário ou senha incorretos.");
			
			var resultado = await _contaService.VerificarSenhaUsuarioAsync(usuario, usuarioLoginDto.Senha);
			if (!resultado.Succeeded) return Unauthorized("Usuário ou senha incorretos.");
			
			return Ok(new
			{
				userName = usuario.UserName,
				primeiroNome = usuario.PrimeiroNome,
				token = _tokenService.CreateToken(usuario).Result
			});
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Erro ao tentar logar usuário. Erro: {e.Message}");
		}
	}
	
	[HttpPut("UpdateUsuario")]
	[AllowAnonymous]
	public async Task<IActionResult> UpdateUsuario(UpdateUsuarioDto usuarioUpdateDto)
	{
		try
		{
			UpdateUsuarioDto? usuario = await _contaService.GetUsuarioByUserName(User.GetUserName() ?? string.Empty);
			if (usuario == null) return Unauthorized("Usuário inválido.");
			
			var usuarioModificado = await _contaService.UpdateConta(usuarioUpdateDto);
			if (usuarioModificado == null) return NoContent();

			return Ok(usuarioModificado);

		}
		catch (Exception e)
		{
			return StatusCode(500, $"Erro ao tentar modificar usuário. Erro: {e.Message}");
		}
	}
}