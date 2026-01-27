using AutoMapper;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos;
using Eventos.Domain;
using Eventos.Persistence.Contratos;

namespace Eventos.Application;

public class EventoService(
	IGeralPersist geralPersister, 
	IEventoPersist eventoPersister,
	IMapper mapper
	)
	: IEventosService
{
	public async Task<EventoDto?> AddEvento(EventoDto model)
	{
		try
		{
			var evento = mapper.Map<Evento>(model);
			geralPersister.Add(evento);

			if (await geralPersister.SaveChangesAsync())
			{
				var eventoResultado = await eventoPersister.GetEventoByIdAsync(evento.Id);
				var eventoDtoResultado = mapper.Map<EventoDto>(eventoResultado);
				
				return eventoDtoResultado;
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
			if (evento.Id == null) throw new Exception("Evento para delete não encontrado.");
			
			geralPersister.Delete(evento);
			return await geralPersister.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<EventoDto?> UpdateEvento(int eventoId, EventoDto model)
	{
		try
		{
			Evento? evento = await eventoPersister.GetEventoByIdAsync(eventoId);
			if (evento == null) return null;

			model.Id = evento.Id;
			mapper.Map(model, evento);

			geralPersister.Update(evento);
			if (await geralPersister.SaveChangesAsync())
			{
				var eventoResultado = await eventoPersister.GetEventoByIdAsync(evento.Id);
				var eventoDtoResultado = mapper.Map<EventoDto>(eventoResultado);
				
				return eventoDtoResultado;
			}

			return null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	
	public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosAsync(includePalestrantes);
			var resultado = mapper.Map<EventoDto[]>(eventos);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosByTemaAsync(tema, includePalestrantes);
			var resultado = mapper.Map<EventoDto[]>(eventos);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<EventoDto?> GetEventoByIdAsync(int id, bool includePalestrantes)
	{
		try
		{
			var evento = await eventoPersister.GetEventoByIdAsync(id, includePalestrantes);
			var resultado = mapper.Map<EventoDto>(evento);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
}