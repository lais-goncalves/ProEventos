using Eventos.Application.Contratos;
using Eventos.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : Controller
{
	private readonly IEventosService _eventosService;
	private readonly IHostEnvironment _hostEnvironment;

	public EventosController(IEventosService eventosService, IHostEnvironment hostEnvironment)
	{
		_eventosService = eventosService;
		_hostEnvironment = hostEnvironment;
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
	
	[HttpGet("id/{id:int}")]
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
	
	[HttpGet("tema/{tema}")]
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
	
	[HttpPost("imagem/{id}")]
	public async Task<ActionResult> PostImagem(int id)
	{
		try
		{
			var evento = await _eventosService.GetEventoByIdAsync(id, false);
			if (evento == null) return NoContent();

			var file = Request.Form.Files[0];
			if (file.Length > 0)
			{
				DeleteImagem(evento.ImagemURL);
				evento.ImagemURL = await SaveImagem(file);
			}

			evento.Id = id;
			var eventoRetorno = await _eventosService.UpdateEvento(id, evento);
			return Ok(eventoRetorno);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar cadastrar evento. Erro: {e.Message}");
		}
	}
	
	[HttpPut("id/{id:int}")]
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
	
	[HttpDelete("id/{id:int}")]
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

	[NonAction]
	public async Task<string> SaveImagem(IFormFile imagem)
	{
		string nomeImagem = Path.GetFileNameWithoutExtension(imagem.FileName);
		
		nomeImagem = new String(nomeImagem.Take(10).ToArray()).Replace(" ", "_");
		var dataAtual = DateTime.UtcNow.ToString("yymmssfff");
		var extensaoImagem = Path.GetExtension(imagem.FileName);
		
		var nomeFinalImagem = $"{nomeImagem}{dataAtual}{extensaoImagem}";
		var caminhoImagem = Path.Combine(_hostEnvironment.ContentRootPath, "Resources/Images", nomeFinalImagem);

		using (var fileStream = new FileStream(caminhoImagem, FileMode.Create))
		{
			await imagem.CopyToAsync(fileStream);
			return nomeFinalImagem;
		}
	}

	[NonAction]
	public bool DeleteImagem(string nomeImagem)
	{
		try
		{
			var caminhoImagem = Path.Combine(_hostEnvironment.ContentRootPath, "Resources/Images", nomeImagem);
			if (System.IO.File.Exists(caminhoImagem))
			{
				System.IO.File.Delete(caminhoImagem);
				return true;
			}

			return false;
		}

		catch (Exception e)
		{
			throw new Exception($"Erro ao tentar deletar imagem. Erro: {e.Message}");
		}
	}
}