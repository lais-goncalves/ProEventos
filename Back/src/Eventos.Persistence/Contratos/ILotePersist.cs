using Eventos.Domain;

namespace Eventos.Persistence.Contratos;

public interface ILotePersist
{
	public Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId);
	public Task<Lote?> GetLoteByIdsAsync(int eventoId, int loteId);
	public Task<bool> EventoExisteAsync(int eventoId);
	public void ExecuteDeleteRangeAsync(int[] modelsIds);
}