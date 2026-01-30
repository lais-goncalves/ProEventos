using Eventos.Application.Dtos;
using Eventos.Domain;

namespace Eventos.Application.Contratos;

public interface IEventosService
{
	Task<EventoDto> AddEvento(EventoDto model);
	Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
	Task<bool> DeleteEvento(int eventoId);
	
	public Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes); 
	public Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
	public Task<EventoDto?> GetEventoByIdAsync(int id, bool includePalestrantes);
}