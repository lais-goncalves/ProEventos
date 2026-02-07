using Eventos.Domain;

namespace Eventos.Persistence.Contratos;

public interface IEventoPersist
{
	public Task<Evento[]> GetAllEventosByTemaAsync(int usuarioId, string tema, bool includePalestrantes = false);
	public Task<Evento[]> GetAllEventosAsync(int usuarioId, bool includePalestrantes = false);
	public Task<Evento?> GetEventoByIdAsync(int usuarioId, int id, bool includePalestrantes = false);
}