using Eventos.Domain;

namespace Eventos.Persistence.Contratos;

public interface IEventoPersist
{
	public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
	public Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
	public Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes);
}