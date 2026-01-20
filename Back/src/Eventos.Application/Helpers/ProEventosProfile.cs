using AutoMapper;
using Eventos.Application.Dtos;
using Eventos.Domain;

namespace Eventos.Application.Helpers;

public class ProEventosProfile : Profile
{
	public ProEventosProfile()
	{
		CreateMap<Evento, EventoDto>().ReverseMap();
		CreateMap<Lote, LoteDto>().ReverseMap();
		CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
		CreateMap<Palestrante, PalestranteDto>().ReverseMap();
	}
}