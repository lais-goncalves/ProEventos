using Eventos.Application.Contratos;
using Eventos.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : Controller
{
	private readonly IEventosService _eventosService;

	public EventosController(IEventosService eventosService)
	{
		_eventosService = eventosService;
	}
	
	[HttpGet]
	public async Task<ActionResult> Get()
	{
		try
		{
			var eventos = await _eventosService.GetAllEventosAsync(true);
			if (eventos == null) return NoContent();
			
			return Ok(eventos);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                       $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
		}
	}
	
	[HttpGet("/id/{id:int}")]
	public async Task<ActionResult> Get(int id)
	{
		try
		{
			var evento = await _eventosService.GetEventoByIdAsync(id, true);
			if (evento == null) return NoContent();
			
			return Ok(evento);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar recuperar evento. Erro: {e.Message}");
		}
	}
	
	[HttpGet("/tema/{tema}")]
	public async Task<ActionResult> Get(string tema)
	{
		try
		{
			var evento = await _eventosService.GetAllEventosByTemaAsync(tema, true);
			if (evento == null) return NoContent();
			
			return Ok(evento);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
		}
	}
	
	[HttpPost]
	public async Task<ActionResult> Post(EventoDto model)
	{
		try
		{
			var evento = await _eventosService.AddEvento(model);
			if (evento == null) return NoContent();
			
			return Ok(evento);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar cadastrar evento. Erro: {e.Message}");
		}
	}
	
	[HttpPut("/id/{id:int}")]
	public async Task<ActionResult> Put(int id, EventoDto model)
	{
		try
		{
			var evento = _eventosService.UpdateEvento(id, model);
			if (evento == null) return NoContent();
			
			return Ok(evento);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar modificar evento. Erro: {e.Message}");
		}
	}
	
	[HttpDelete("/id/{id:int}")]
	public async Task<ActionResult> Delete(int id)
	{
		try
		{
			var deletouEvento = await _eventosService.DeleteEvento(id);
			if (!deletouEvento) return NoContent();
			
			return Ok("Evento deletado com sucesso.");
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar deletar evento. Erro: {e.Message}");
		}
	}
}