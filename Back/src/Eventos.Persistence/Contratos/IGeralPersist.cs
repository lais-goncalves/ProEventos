namespace Eventos.Persistence.Contratos;

public interface IGeralPersist
{
	public void Add<T>(T entity);
	public void Update<T>(T entity);
	public void Delete<T>(T entity);
	public void DeleteRange<T>(T[] entities);
	
	public Task<bool> SaveChangesAsync();
}