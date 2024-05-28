using System.Collections.Generic;

namespace AUTOGLASS_RodrigoLuz.Domain.DTO
{
	public class ListaProdutoResponseDto : BaseResponseDto
	{
		public List<ProdutoDto> Produtos { get; set; }
	}
}

