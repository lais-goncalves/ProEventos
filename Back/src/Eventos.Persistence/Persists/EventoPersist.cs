using Eventos.Domain;
using Eventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence.Persists;

public class EventoPersist(EventosContext context) : IEventoPersist
{
	public async Task<Evento[]> GetAllEventosAsync(int usuarioId, bool includePalestrantes = false)
	{
		IQueryable<Evento> query = context.Eventos
		                                  .Include(e => e.Lotes)
		                                  .Include(e => e.RedesSociais);

		if (includePalestrantes)
		{
			query = query
			        .Include(e => e.PalestrantesEventos)
			        .ThenInclude(pe => pe.Palestrante);
		}
		
		query = query
			        .AsNoTracking()
			        .Where(e => e.UsuarioId == usuarioId)
			        .OrderBy(e => e.Id);
		
		return await query.ToArrayAsync();
	}

	public async Task<Evento[]> GetAllEventosByTemaAsync(int usuarioId, string tema, bool includePalestrantes = false)
	{
		IQueryable<Evento> 
			query = context.Eventos
		                                  .Include(e => e.Lotes)
		                                  .Include(e => e.RedesSociais);

		if (includePalestrantes)
		{
			query = query
			        .Include(e => e.PalestrantesEventos)
			        .ThenInclude(pe => pe.Palestrante);
		}
		
		query = query
		        .AsNoTracking()
		        .Where(e => 
			               e.Tema.ToLower().Contains(tema.ToLower()) &&
			               e.UsuarioId == usuarioId
		              )
		        .OrderBy(e => e.Id);

		return await query.ToArrayAsync();
	}

	public async Task<Evento?> GetEventoByIdAsync(int usuarioId, int id, bool includePalestrantes = false)
	{
		IQueryable<Evento?> query = context.Eventos
		                                  .Include(e => e.Lotes)
		                                  .Include(e => e.RedesSociais);

		if (includePalestrantes)
		{
			query = query
			        .Include(e => e.PalestrantesEventos)
			        .ThenInclude(pe => pe.Palestrante);
		}

		query = query
		        .AsNoTracking()
		        .Where(e => e.Id == id && e.UsuarioId == usuarioId);
		
		return await query.FirstOrDefaultAsync();
	}
}