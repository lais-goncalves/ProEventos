using System.Diagnostics;
using Eventos.Api.Data;
using Eventos.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class EventosController(DataContext contexto) : Controller
{
	DataContext contexto =  contexto;
	
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