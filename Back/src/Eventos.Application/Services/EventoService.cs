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
	public async Task<EventoDto?> AddEvento(int usuarioId, EventoDto model)
	{
		try
		{
			var evento = mapper.Map<Evento>(model);
			evento.UsuarioId = usuarioId;
			geralPersister.Add(evento);

			if (await geralPersister.SaveChangesAsync())
			{
				var eventoResultado = await eventoPersister.GetEventoByIdAsync(usuarioId, evento.Id);
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
	
	public async Task<bool> DeleteEvento(int usuarioId, int eventoId)
	{
		try
		{
			Evento? evento = await eventoPersister.GetEventoByIdAsync(usuarioId, eventoId);
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
	
	public async Task<EventoDto?> UpdateEvento(int usuarioId, int eventoId, EventoDto model)
	{
		try
		{
			Evento? evento = await eventoPersister.GetEventoByIdAsync(usuarioId, eventoId);
			if (evento == null) return null;

			model.Id = evento.Id;
			model.UsuarioId = usuarioId;
			mapper.Map(model, evento);

			geralPersister.Update(evento);
			if (await geralPersister.SaveChangesAsync())
			{
				var eventoResultado = await eventoPersister.GetEventoByIdAsync(usuarioId, evento.Id);
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
	
	
	public async Task<EventoDto[]> GetAllEventosAsync(int usuarioId, bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosAsync(usuarioId, includePalestrantes);
			var resultado = mapper.Map<EventoDto[]>(eventos);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<EventoDto[]> GetAllEventosByTemaAsync(int usuarioId, string tema, bool includePalestrantes)
	{
		try
		{
			var eventos = await eventoPersister.GetAllEventosByTemaAsync(usuarioId, tema, includePalestrantes);
			var resultado = mapper.Map<EventoDto[]>(eventos);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<EventoDto?> GetEventoByIdAsync(int usuarioId, int id, bool includePalestrantes)
	{
		try
		{
			var evento = await eventoPersister.GetEventoByIdAsync(usuarioId, id, includePalestrantes);
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