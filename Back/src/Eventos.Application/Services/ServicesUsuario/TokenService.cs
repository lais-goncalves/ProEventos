using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos.Usuario;
using Eventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eventos.Application.ServicesUsuario;

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;
	private readonly UserManager<Usuario> _userManager;
	private readonly IMapper _mapper;
	private readonly SymmetricSecurityKey _key;

	public TokenService(
		IConfiguration config,
        UserManager<Usuario> userManager,
        IMapper mapper
		)
	{
		_config = config;
		_userManager = userManager;
		_mapper = mapper;
		_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
	}
	
	public async Task<string> CreateToken(UpdateUsuarioDto updateUsuarioDto)
	{
		Usuario usuario = _mapper.Map<Usuario>(updateUsuarioDto);

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
			new Claim(ClaimTypes.Name, usuario.UserName ?? "")
		};

		var papeis = await _userManager.GetRolesAsync(usuario);
		claims.AddRange(papeis.Select(p => new Claim(ClaimTypes.Role, p)));

		var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

		var tokenDescricao = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.Now.AddDays(1),
			SigningCredentials = creds
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescricao);
		
		return tokenHandler.WriteToken(token);
	}
}