using Xunit;
using Moq;
using AUTOGLASS_RodrigoLuz.Application.Services;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Constants;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using System;

namespace AUTOGLASS_RodrigoLuz.Tests.Services
{
	public class ProdutoServiceRecuperaTests
	{
        ProdutoService _produtoService;

        Mock<IMapper> _mapper;
        Mock<IProdutoRepository> _produtoRepository;

		Produto produtoEntity = new Produto(1, "Produto Teste", true, DateTime.Now, DateTime.Now.AddYears(1), 1);
		ProdutoDto produtoDto = new ProdutoDto() { CodigoProduto = 1, Situacao = true, Descricao = "Produto Teste",
								DataFabricacao = DateTime.Now, DataValidade = DateTime.Now,
								CodigoFornecedor = 1, DescricaoFornecedor="Fornecedor", CNPJFornecedor="123123123123produtoDto"};

        public ProdutoServiceRecuperaTests()
		{
			_mapper = new Mock<IMapper>();
			_produtoRepository = new Mock<IProdutoRepository>();
		}


        [Fact]
		public async Task RecupeProdutoPorCodigo_Success_Test()
		{
            _mapper.Setup(m => m.Map<ProdutoDto>(It.IsAny<Produto>())).Returns(produtoDto);
            _produtoRepository.Setup(p => p.RecuperaProdutoPorCodigo(It.IsAny<int>())).ReturnsAsync(produtoEntity);

			_produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

			var resultado = await _produtoService.RecuperaProdutoPorCodigo(1);

			Assert.NotNull(resultado);
			Assert.Equal(resultado.Code, ResponseStatusCodes.Success);
			Assert.Equal(resultado.Produto, produtoDto);

        }


        [Fact]
        public async Task RecupeProdutoPorCodigo_NotFound_Test()
        {
			var msgErro = "Produto não encontrado";

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.RecuperaProdutoPorCodigo(1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.NotFound);
            Assert.Equal(resultado.Message, msgErro);

        }


        [Fact]
        public async Task RecupeProdutoPorCodigo_Error_Test()
        {
            var msgErro = "Erro ao buscar produto";

            _produtoRepository.Setup(p => p.RecuperaProdutoPorCodigo(It.IsAny<int>())).Throws(new Exception(msgErro));

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.RecuperaProdutoPorCodigo(1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }

       
    }

}

