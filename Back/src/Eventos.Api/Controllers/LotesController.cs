using Eventos.Application.Dtos;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Lotes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotesController : Controller
{
	private readonly ILotesService _lotesService;

	public LotesController(ILotesService lotesService)
	{
		_lotesService = lotesService;
	}
	
	[HttpGet("evento_id/{eventoId}")]
	public async Task<ActionResult> Get(int eventoId)
	{
		try
		{
			var lotes = await _lotesService.GetLotesByEventoIdAsync(eventoId);
			if (lotes == null) return NoContent();
			
			return Ok(lotes);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar recuperar lotes. Erro: {e.Message}");
		}
	}
	
	[HttpPut("evento_id/{eventoId:int}")]
	public async Task<ActionResult> Put(int eventoId, LoteDto[] models)
	{
		try
		{
			var lote = _lotesService.SaveLotesAsync(eventoId, models);
			if (lote == null) return NoContent();
			
			return Ok(lote);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar modificar lote. Erro: {e.Message}");
		}
	}
	
	[HttpDelete("evento_id/{eventoId:int}/lote_id/{loteId:int}")]
	public async Task<ActionResult> Delete(int eventoId, int loteId)
	{
		try
		{
			var deletouLote = await _lotesService.DeleteLoteAsync(eventoId, loteId);
			if (!deletouLote) return NoContent();
			
			return Ok("Lote deletado com sucesso.");
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
			                  $"Erro ao tentar deletar lote. Erro: {e.Message}");
		}
	}
}