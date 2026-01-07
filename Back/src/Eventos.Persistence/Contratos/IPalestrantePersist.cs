using Eventos.Domain;

namespace Eventos.Persistence.Contratos;

public interface IPalestrantePersist
{
	public Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
	public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
	public Task<Palestrante?> GetPalestranteByIdAsync(int id, bool includeEventos);
}