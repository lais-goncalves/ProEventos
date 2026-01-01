using System.Diagnostics;
using Eventos.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class EventoController : Controller
{
	public IEnumerable<Evento> _eventos =
	[
		new()
		{
			EventoId = 1,
			Tema = "Angular e .NET",
			Local = "São Paulo",
			Lote = "1o lote",
			QtdPessoas = 250,
			DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
		},
		new()
		{
			EventoId = 2,
			Tema = "Angular e .NET",
			Local = "São Paulo",
			Lote = "2o lote",
			QtdPessoas = 250,
			DataEvento = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy")
		}
	];
	
	[HttpGet]
	public IEnumerable<Evento> Get()
	{
		return _eventos;
	}
	
	[HttpGet("{id:int}")]
	public IEnumerable<Evento> GetById(int id)
	{
		return _eventos.Where(e => e.EventoId == id);
	}
}