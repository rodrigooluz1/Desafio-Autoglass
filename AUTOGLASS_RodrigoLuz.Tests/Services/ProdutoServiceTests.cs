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
	public class ProdutoServiceTests
	{
        ProdutoService _produtoService;

        Mock<IMapper> _mapper;
        Mock<IProdutoRepository> _produtoRepository;

		Produto produtoEntity = new Produto(1, "Produto Teste", true, DateTime.Now, DateTime.Now.AddYears(1), 1);
		ProdutoDto produtoDto = new ProdutoDto() { CodigoProduto = 1, Situacao = true, Descricao = "Produto Teste",
								DataFabricacao = DateTime.Now, DataValidade = DateTime.Now,
								CodigoFornecedor = 1, DescricaoFornecedor="Fornecedor", CNPJFornecedor="123123123123produtoDto"};

        public ProdutoServiceTests()
		{
			_mapper = new Mock<IMapper>();
			_produtoRepository = new Mock<IProdutoRepository>();
		}


        #region RECUPERA_PRODUTO_POR_CODIGO

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

        #endregion

        #region LISTA_PRODUTOS

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

        #endregion


        #region INSERE PRODUTO

        [Fact]
        public async Task InsereProduto_Success_Test()
        {
            _mapper.Setup(m => m.Map<ProdutoDto>(It.IsAny<Produto>())).Returns(produtoDto);
            _produtoRepository.Setup(p => p.InsereProduto(It.IsAny<Produto>())).ReturnsAsync(produtoEntity);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.InsereProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Success);
            Assert.Equal(resultado.Produto, produtoDto);

        }


        [Fact]
        public async Task InsereProduto_ValidaDataFabricacao_Test()
        {
            var msgErro = "A data de fabricação não pode ser maior ou igual a data de validade";
            produtoDto.DataFabricacao = DateTime.Now.AddYears(3);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.InsereProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }


        [Fact]
        public async Task InsereProduto_Error_Test()
        {
            var msgErro = "Erro ao inserir produto";

            _produtoRepository.Setup(p => p.InsereProduto(It.IsAny<Produto>())).Throws(new Exception(msgErro));

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.InsereProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }

        #endregion


        #region EDITA PRODUTO

        [Fact]
        public async Task EditaProduto_Success_Test()
        {
            var msg = "Produto editado com sucesso";
            _produtoRepository.Setup(p => p.AtualizaProduto(It.IsAny<Produto>())).ReturnsAsync(true);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.EditaProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Success);
            Assert.Equal(resultado.Message, msg);

        }


        [Fact]
        public async Task EditaProduto_ValidaDataFabricacao_Test()
        {
            var msgErro = "A data de fabricação não pode ser maior ou igual a data de validade";
            produtoDto.DataFabricacao = DateTime.Now.AddYears(3);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.EditaProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }


        [Fact]
        public async Task EditaProduto_Error_Test()
        {
            var msgErro = "Erro ao editar produto.";

            _produtoRepository.Setup(p => p.AtualizaProduto(It.IsAny<Produto>())).Throws(new Exception(msgErro));

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.EditaProduto(produtoDto);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }

        #endregion

        #region EXCLUIR PRODUTO

        [Fact]
        public async Task ExcluiProduto_Success_Test()
        {
            var msg = "Produto deletado";

            _produtoRepository.Setup(p => p.RecuperaProdutoPorCodigo(It.IsAny<int>())).ReturnsAsync(produtoEntity);
            _produtoRepository.Setup(p => p.AtualizaProduto(It.IsAny<Produto>())).ReturnsAsync(true);

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ExcluiProduto(1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Success);
            Assert.Equal(resultado.Message, msg);

        }


        [Fact]
        public async Task ExcluiProduto_NotFound_Test()
        {
            var msgErro = "Produto não encontrado";

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ExcluiProduto(1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.NotFound);
            Assert.Equal(resultado.Message, msgErro);

        }


        [Fact]
        public async Task ExcluiProduto_Error_Test()
        {
            var msgErro = "Erro ao deletar produto.";

            _produtoRepository.Setup(p => p.RecuperaProdutoPorCodigo(It.IsAny<int>())).ReturnsAsync(produtoEntity);
            _produtoRepository.Setup(p => p.AtualizaProduto(It.IsAny<Produto>())).Throws(new Exception(msgErro));

            _produtoService = new ProdutoService(_mapper.Object, _produtoRepository.Object);

            var resultado = await _produtoService.ExcluiProduto(1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Code, ResponseStatusCodes.Error);
            Assert.Equal(resultado.Message, msgErro);

        }

        #endregion
    }

}

