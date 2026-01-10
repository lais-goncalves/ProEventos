using Eventos.Application.Contratos;
using Eventos.Domain;
using Eventos.Persistence.Contratos;

namespace Eventos.Application;

public class EventoService(IGeralPersist geralPersister, IEventoPersist eventoPersister)
	: IEventosService
{
	public async Task<Evento?> AddEvento(Evento model)
	{
		try
		{
			geralPersister.Add(model);

			if (await geralPersister.SaveChangesAsync())
			{
				return await eventoPersister.GetEventoByIdAsync(model.Id);
			}

			return null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<bool> DeleteEvento(int eventoId)
	{
		try
		{
			Evento? evento = await eventoPersister.GetEventoByIdAsync(eventoId);
			if (evento == null) throw new Exception("Evento para delete não encontrado.");
			
			geralPersister.Delete(evento);
			return await geralPersister.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<Evento?> UpdateEvento(int eventoId, Evento model)
	{
		try
		{
			Evento? evento = await eventoPersister.GetEventoByIdAsync(eventoId);
			if (evento == null) return null;

			model.Id = evento.Id;

			geralPersister.Update(model);
			if (await geralPersister.SaveChangesAsync())
			{
				return await eventoPersister.GetEventoByIdAsync(eventoId);
			}

			return null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	
	public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosAsync(includePalestrantes);
			return eventos;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosByTemaAsync(tema, includePalestrantes);
			return eventos;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes)
	{
		try
		{
			var evento = await eventoPersister.GetEventoByIdAsync(id, includePalestrantes);
			return evento;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
}