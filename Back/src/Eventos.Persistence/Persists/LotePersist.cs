using Eventos.Domain;
using Eventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence.Persists;

public class LotePersist(EventosContext context) : ILotePersist
{
	public async Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId)
	{
		IQueryable<Lote> query = context.Lotes;
		query = query
					.AsNoTracking()
					.Where(l => l.EventoId == eventoId)
					.OrderBy(l => l.Id);
		
		return await query.ToArrayAsync();
	}
	
	public async Task<Lote?> GetLoteByIdsAsync(int eventoId, int loteId)
	{
		IQueryable<Lote> query = context.Lotes;
		query = query
			        .AsNoTracking()
					.Where(l => l.EventoId == eventoId && l.Id == loteId);
		
		return await query.FirstOrDefaultAsync();
	}

	public async Task<bool> EventoExisteAsync(int eventoId)
	{
		IQueryable<Evento> query = context.Eventos;
		
		Evento? evento = await query
		        .AsNoTracking()
		        .FirstOrDefaultAsync(e => e.Id == eventoId);
		
		return evento != null;
	}
	
	public void ExecuteDeleteRangeAsync(int[] modelsIds)
	{
		context.Lotes
		             .Where(l => modelsIds.Contains(l.Id))
		             .ExecuteDeleteAsync();
	}
}