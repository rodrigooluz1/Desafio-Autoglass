using System;
namespace AUTOGLASS_RodrigoLuz.Domain.DTO
{
	public class ProdutoDto
	{
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJFornecedor { get; set; }
    }
}

