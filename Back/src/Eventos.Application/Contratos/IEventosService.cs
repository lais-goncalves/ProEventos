using Eventos.Application.Dtos;

namespace Eventos.Application.Contratos;

public interface IEventosService
{
	Task<EventoDto> AddEvento(int usuarioId, EventoDto model);
	Task<EventoDto> UpdateEvento(int usuarioId, int eventoId, EventoDto model);
	Task<bool> DeleteEvento(int usuarioId, int eventoId);
	
	public Task<EventoDto[]> GetAllEventosAsync(int usuarioId, bool includePalestrantes); 
	public Task<EventoDto[]> GetAllEventosByTemaAsync(int usuarioId, string tema, bool includePalestrantes);
	public Task<EventoDto?> GetEventoByIdAsync(int usuarioId, int id, bool includePalestrantes);
}