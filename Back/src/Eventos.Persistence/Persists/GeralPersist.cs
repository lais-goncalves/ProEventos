using Eventos.Persistence.Contratos;

namespace Eventos.Persistence.Persists;

public class GeralPersist(EventosContext context) : IGeralPersist
{
	public void Add<T>(T entity)
	{
		if (entity != null) context.Add(entity);
	}

	public void Update<T>(T entity)
	{
		if (entity != null) context.Update(entity);
	}
	
	public void Delete<T>(T entity) 
	{
		if (entity != null) context.Remove(entity);
	}
	
	public void DeleteRange<T>(T[] entities)
	{
		context.RemoveRange(entities);
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await context.SaveChangesAsync() > 0;
	}
}