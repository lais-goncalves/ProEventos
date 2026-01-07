using Eventos.Domain;
using Eventos.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class EventosController(EventosContext contexto) : Controller
{
	EventosContext contexto =  contexto;
	
	[HttpGet]
	public IEnumerable<Evento> Get()
	{
		return contexto.Eventos.OrderBy(e => e.EventoId);
	}
	
	[HttpGet("{id:int}")]
	public Evento? GetById(int id)
	{
		return contexto.Eventos.FirstOrDefault(e => e.EventoId == id);
	}
}