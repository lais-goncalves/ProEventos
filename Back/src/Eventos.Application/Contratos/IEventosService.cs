using Eventos.Domain;

namespace Eventos.Application.Contratos;

public interface IEventosService
{
	Task<Evento> AddEvento(Evento model);
	Task<Evento> UpdateEvento(int eventoId, Evento model);
	Task<bool> DeleteEvento(int eventoId);
	
	public Task<Evento[]> GetAllEventosAsync(bool includePalestrantes); 
	public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
	public Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes);
}