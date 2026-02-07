using AutoMapper;
using Eventos.Application.Dtos;
using Eventos.Application.Dtos.Usuario;
using Eventos.Domain;
using Eventos.Domain.Identity;

namespace Eventos.Application.Helpers;

public class ProEventosProfile : Profile
{
	public ProEventosProfile()
	{
		CreateMap<Evento, EventoDto>().ReverseMap();
		CreateMap<Lote, LoteDto>().ReverseMap();
		CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
		CreateMap<Palestrante, PalestranteDto>().ReverseMap();
		CreateMap<Usuario, UsuarioDto>().ReverseMap();
		CreateMap<Usuario, UpdateUsuarioDto>().ReverseMap();
	}
}