using Eventos.Application.Dtos;
using Eventos.Domain;

namespace Eventos.Application.Contratos;

public interface ILotesService
{
	public Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId); 
	public Task<LoteDto?> GetLoteByIdsAsync(int eventoId, int loteId);
	
	Task<LoteDto[]> SaveLotesAsync(int eventoId, LoteDto[] model);
	Task<bool> DeleteLoteAsync(int eventoId, int loteId);
}
