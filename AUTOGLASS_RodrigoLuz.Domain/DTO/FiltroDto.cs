using System;

namespace AUTOGLASS_RodrigoLuz.Domain.DTO
{
	public class FiltroDto
	{
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }

        public int? CodigoProduto { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? CodigoFornecedor { get; set; }
        public string? DescricaoFornecedor { get; set; }
        public string? CNPJFornecedor { get; set; }

    }
}

