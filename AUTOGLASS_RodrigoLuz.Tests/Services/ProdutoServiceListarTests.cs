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
using System.Collections.Generic;

namespace AUTOGLASS_RodrigoLuz.Tests.Services
{
	public class ProdutoServiceListarTests
	{
        ProdutoService _produtoService;

        Mock<IMapper> _mapper;
        Mock<IProdutoRepository> _produtoRepository;

		Produto produtoEntity = new Produto(1, "Produto Teste", true, DateTime.Now, DateTime.Now.AddYears(1), 1);
		ProdutoDto produtoDto = new ProdutoDto() { CodigoProduto = 1, Situacao = true, Descricao = "Produto Teste",
								DataFabricacao = DateTime.Now, DataValidade = DateTime.Now,
								CodigoFornecedor = 1, DescricaoFornecedor="Fornecedor", CNPJFornecedor="123123123123produtoDto"};

        public ProdutoServiceListarTests()
		{
			_mapper = new Mock<IMapper>();
			_produtoRepository = new Mock<IProdutoRepository>();
		}

        [Fact]
        public async Task ListarProdutos_Success_Test()
        {
            var listaEntity = new List<Produto> { produtoEntity };
            var listaDto = new List<ProdutoDto> { produtoDto };
            var filtro = new FiltroDto() { NumeroPagina = 1, TamanhoPagina = 1 };

            _mapper.Setup(m => m.Map<List<ProdutoDto>>(It.IsAny<List<Produto>>())).Returns(listaDto);
            _produtoRepository.Setup(p => p.ListarProdutos(It.IsAny<FiltroDto>())).ReturnsAsync(listaEntity);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ListarProdutos(filtro);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Success);
            Assert.Equal(resultado.Produtos, listaDto);

        }


        [Fact]
        public async Task ListarProdutos_NotFound_Test()
        {
            var msgErro = "Nenhum produto na lista";
            var filtro = new FiltroDto() { NumeroPagina = 1, TamanhoPagina = 1 };

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ListarProdutos(filtro);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.NotFound);
            Assert.Equal(resultado.Message, msgErro);

        }


        [Fact]
        public async Task ListarProdutos_NotFound_Error_Test()
        {
            var msgErro = "Erro ao listar produtos";
            var filtro = new FiltroDto() { NumeroPagina = 1, TamanhoPagina = 1 };

            _produtoRepository.Setup(p => p.ListarProdutos(It.IsAny<FiltroDto>())).Throws(new Exception(msgErro));

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ListarProdutos(filtro);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }


    }

}

