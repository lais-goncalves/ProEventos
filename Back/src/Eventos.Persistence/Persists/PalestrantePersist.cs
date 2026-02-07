using Eventos.Domain;
using Eventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence.Persists;

public class PalestrantePersist(EventosContext context) : IPalestrantePersist
{
	public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
	{
		IQueryable<Palestrante> query = context.Palestrantes
		                                       .Include(p => p.RedeSociais);

		if (includeEventos)
		{
			query = query
			        .AsNoTracking()
			        .Include(p => p.PalestranteEventos)
			        .ThenInclude(pe => pe.Evento);
		}
		
		query = query.AsNoTracking().OrderBy(p => p.Id);
		
		return await query.ToArrayAsync();
	}
	
	public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
	{
		IQueryable<Palestrante> query = context.Palestrantes
		                                       .Include(p => p.RedeSociais);

		if (includeEventos)
		{
			query = query
			        .Include(p => p.Usuario)
			        .ThenInclude(u => u.PrimeiroNome)
			        .Include(p => p.PalestranteEventos)
			        .ThenInclude(pe => pe.Evento);
		}
		
		query = query
					.AsNoTracking()
					.Where(p => p.Usuario.PrimeiroNome.Contains(nome, StringComparison.CurrentCultureIgnoreCase))
					.OrderBy(p => p.Id);
		
		return await query.ToArrayAsync();
	}

	public async Task<Palestrante?> GetPalestranteByIdAsync(int id, bool includeEventos = false)
	{
		IQueryable<Palestrante> query = context.Palestrantes
		                                       .Include(p => p.RedeSociais);

		if (includeEventos)
		{
			query = query
			        .Include(p => p.PalestranteEventos)
			        .ThenInclude(pe => pe.Evento);
		}

		query = query.AsNoTracking().Where(p => p.Id == id);
		
		return await query.FirstOrDefaultAsync();
	}
}