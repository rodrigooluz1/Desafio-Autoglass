using System;
using AUTOGLASS_RodrigoLuz.Domain.Entities.Util;

namespace AUTOGLASS_RodrigoLuz.Domain.Entities
{
	public class Produto
	{

        public Produto(int id, string descricao, bool situacao, DateTime dataFabricacao,
                        DateTime dataValidade, int idFornecedor)
        {
            Id = id;
            Descricao = descricao;
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            IdFornecedor = idFornecedor;

            ValidaEntity();

        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public bool Situacao { get; set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public int IdFornecedor { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        public void ValidaEntity()
        {
            Validacao.VerificaCampoVazio(Descricao, "A descrição do produto não pode estar vazia!");
            Validacao.VerificaDataFabricacao(DataFabricacao, DataValidade, "A data de fabricação não pode ser maior ou igual a data de validade");
        }
    }
}

