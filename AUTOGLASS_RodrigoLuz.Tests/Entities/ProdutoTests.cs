using System;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using AUTOGLASS_RodrigoLuz.Domain.Entities.Util;
using Xunit;

namespace YourNamespace.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Dados_Validos_Test()
        {
            // Arrange
            int id = 1;
            string descricao = "Produto Teste";
            bool situacao = true;
            DateTime dataFabricacao = DateTime.Now;
            DateTime dataValidade = DateTime.Now.AddYears(1);
            int idFornecedor = 1;

            // Act
            var produto = new Produto(id, descricao, situacao, dataFabricacao, dataValidade, idFornecedor);

            // Assert
            Assert.Equal(id, produto.Id);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(situacao, produto.Situacao);
            Assert.Equal(dataFabricacao, produto.DataFabricacao);
            Assert.Equal(dataValidade, produto.DataValidade);
            Assert.Equal(idFornecedor, produto.IdFornecedor);
        }

        [Fact]
        public void Produto_Descricao_Vazia_Test()
        {
            // Arrange
            int id = 1;
            string descricao = "";
            bool situacao = true;
            DateTime dataFabricacao = DateTime.Now;
            DateTime dataValidade = DateTime.Now.AddYears(1);
            int idFornecedor = 1;

            // Act | Assert
            
            var exception = Assert.Throws<DomainException>(() =>
                new Produto(id, descricao, situacao, dataFabricacao, dataValidade, idFornecedor));

            Assert.Equal("A descrição do produto não pode estar vazia!", exception.Message);

             
        }

        [Fact]
        public void Produto_Valida_DataFabricacao_Test()
        {
            // Arrange
            int id = 1;
            string descricao = "Produto Teste";
            bool situacao = true;
            DateTime dataFabricacao = DateTime.Now;
            DateTime dataValidade = new DateTime(2023, 1, 1);
            int idFornecedor = 1;

            // Act | Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Produto(id, descricao, situacao, dataFabricacao, dataValidade, idFornecedor));

            Assert.Equal("A data de fabricação não pode ser maior ou igual a data de validade", exception.Message);
        }
    }
}

