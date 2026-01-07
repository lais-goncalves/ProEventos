using Eventos.Domain;
using Eventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence.Persists;

public class EventoPersist(EventosContext context) : IEventoPersist
{
	public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
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
		
		query = query.OrderBy(e => e.EventoId);
		
		return await query.ToArrayAsync();
	}

	public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
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
		        .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
		        .OrderBy(e => e.EventoId);

		return await query.ToArrayAsync();
	}

	public async Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes = false)
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

		query = query.Where(e => e.EventoId == id);
		
		return await query.FirstOrDefaultAsync();
	}
}