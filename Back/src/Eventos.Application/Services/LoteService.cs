using AutoMapper;
using Eventos.Application.Contratos;
using Eventos.Application.Dtos;
using Eventos.Domain;
using Eventos.Persistence.Contratos;

namespace Eventos.Application;

public class LoteService(
	IGeralPersist geralPersister, 
	ILotePersist lotePersister,
	IMapper mapper
	)
	: ILotesService
{
	
	public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
	{
		try
		{
			var lotes = await lotePersister.GetAllLotesByEventoIdAsync(eventoId);
			var resultado = mapper.Map<LoteDto[]>(lotes);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}

	public async Task<LoteDto?> GetLoteByIdsAsync(int eventoId, int loteId)
	{
		try
		{
			var lote = await lotePersister.GetLoteByIdsAsync(eventoId, loteId);
			var resultado = mapper.Map<LoteDto>(lote);
			
			return resultado;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}

	public async Task<LoteDto[]> SaveLotesAsync(int eventoId, LoteDto[] models)
	{
		try
		{
			bool eventoExiste = await lotePersister.EventoExisteAsync(eventoId);
			if (!eventoExiste)
			{
				return [];
			}
			
			// adiciona ou atualiza lotes
			List<LoteDto> lotesResultantes = [];
			foreach (var model in models)
			{
				LoteDto? loteAdicionado;
					
				if (model.Id == 0)
				{
					loteAdicionado = await AddLoteAsync(eventoId, model);
				}
				else
				{
					loteAdicionado = await UpdateLoteAsync(eventoId, model.Id, model);
				}

				if (loteAdicionado != null)
				{
					lotesResultantes.Add(loteAdicionado);
				}
			}

			// remove lotes que não estão na lista para serem atualizados ou deletados
			// ou seja, quando um lote é exluído da lista no frontend, ele vai ser excluído no backend
			int[] idsLotesDeletados = await GetLotesNaoIncluidos(eventoId, lotesResultantes);
			lotePersister.ExecuteDeleteRangeAsync(idsLotesDeletados);

			return lotesResultantes.ToArray();
		}
		
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	public async Task<bool> DeleteLoteAsync(int eventoId, int loteId)
	{
		try
		{
			Lote? lote = await lotePersister.GetLoteByIdsAsync(eventoId, loteId);
			if (lote?.Id == null) throw new Exception("Lote para delete não encontrado.");
			
			geralPersister.Delete(lote);
			return await geralPersister.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new Exception(e.Message);
		}
	}
	
	private async Task DeleteLotesRangeAsync(IEnumerable<Lote> lotes)
	{
		geralPersister.DeleteRange(lotes.ToArray());
	}

	private async Task<int[]> GetLotesNaoIncluidos(int eventoId, IEnumerable<LoteDto> lotesIncluidos) {
		LoteDto[] lotesEventoDto = await GetLotesByEventoIdAsync(eventoId);
		Lote[] lotesEvento = mapper.Map<Lote[]>(lotesEventoDto);
		
		IEnumerable<Lote> lotesNaoIncluidos = lotesEvento.ExceptBy(
		                                                           lotesIncluidos.Select(l => l.Id), 
		                                                           l => l.Id
		                                                          );

		return lotesNaoIncluidos.Select(l => l.Id).ToArray();
	}
	
	private async Task<LoteDto> UpdateLoteAsync(int eventoId, int loteId, LoteDto model)
	{
		try
		{
			LoteDto? loteDto = await GetLoteByIdsAsync(eventoId, loteId);
			if (loteDto == null)
			{
				throw new Exception($"Lote #{loteId} não encontrado.");
			}
			
			Lote? lote = mapper.Map<Lote>(loteDto);
			model.Id = loteId;
			model.EventoId = eventoId;
			mapper.Map(model, lote);
			
			geralPersister.Update(lote);
			bool salvou = await geralPersister.SaveChangesAsync();
			
			if (!salvou)
			{
				throw new Exception($"Não foi possível salvar alterações do lote #{loteId}. Tente novamente.");
			}

			LoteDto? loteSalvo = await GetLoteByIdsAsync(eventoId, loteId);
			return loteSalvo;
		}

		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw ex;
		}
	}

	private async Task<LoteDto?> AddLoteAsync(int eventoId, LoteDto model)
	{
		try
		{
			Lote? lote = mapper.Map<Lote>(model);
			lote.EventoId = eventoId;
			
			geralPersister.Add(lote);
			bool salvou = await geralPersister.SaveChangesAsync();
			
			if (!salvou)
			{
				throw new Exception("Não foi possível cadastrar lote. Tente novamente");
			}

			LoteDto? loteCadastrado = await GetLoteByIdsAsync(eventoId, lote.Id);
			return loteCadastrado;
		}
		
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw ex;
		}
	}
}
