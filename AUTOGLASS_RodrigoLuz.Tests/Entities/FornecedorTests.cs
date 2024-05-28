using AUTOGLASS_RodrigoLuz.Domain.Entities;
using Xunit;

namespace AUTOGLASS_RodrigoLuz.Tests.Entities
{
	public class FornecedorTests
	{
		public FornecedorTests()
		{
		}


        [Fact]
        public void Fornecedor_DadosValidos()
        {
            // Arrange
            string descricaoFornecedor = "Fornecedor Teste";
            string cnpjFornecedor = "12345678000199";

            // Act
            var fornecedor = new Fornecedor() { Id=1, DescricaoFornecedor=descricaoFornecedor, CNPJFornecedor=cnpjFornecedor};

            // Assert
            Assert.Equal(descricaoFornecedor, fornecedor.DescricaoFornecedor);
            Assert.Equal(cnpjFornecedor, fornecedor.CNPJFornecedor);
        }
    }
}

