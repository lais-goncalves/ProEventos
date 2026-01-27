using System.ComponentModel.DataAnnotations;

namespace Eventos.Application.Dtos;

public class EventoDto
{
	public int Id { get; set; }
	public string Local { get; set; }
	public DateTime? DataEvento { get; set; }
	
	[Required(ErrorMessage = "O campo {0} é obrigatório."),
	 StringLength(50, MinimumLength = 4, ErrorMessage = "{0} deve ter no mínimo 3 caracteres e no máximo 50.")]
	public string Tema { get; set; }
	
	[Range(1, 120000, ErrorMessage = "O campo {0} é obrigatório."),
	 Display(Name = "Quantidade de Pessoas")]
	public int QtdPessoas { get; set; }
	
	[RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida (gif, jpg, jpeg, bmp, png)")]
	public string ImagemURL { get; set; }
	
	[Required(ErrorMessage = "O campo {0} é obrigatório."),
	 Display(Name = "E-mail"),
	 EmailAddress(ErrorMessage = "O campo {0} deve ser válido.")]
	public string? Email { get; set; }
	
	[Required(ErrorMessage = "O campo {0} é obrigatório."),
	 Phone(ErrorMessage = "O campo {0} deve ser válido.")]
	public string? Telefone { get; set; }
	
	public IEnumerable<LoteDto>? Lotes { get; set; }
	public IEnumerable<RedeSocialDto>? RedesSociais { get; set; }
	public IEnumerable<PalestranteDto>? Palestrantes { get; set; }
}