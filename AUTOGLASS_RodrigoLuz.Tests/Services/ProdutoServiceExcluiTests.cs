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
	public class ProdutoServiceExcluiTests
	{
        ProdutoService _produtoService;

        Mock<IMapper> _mapper;
        Mock<IProdutoRepository> _produtoRepository;

		Produto produtoEntity = new Produto(1, "Produto Teste", true, DateTime.Now, DateTime.Now.AddYears(1), 1);
		

        public ProdutoServiceExcluiTests()
		{
			_mapper = new Mock<IMapper>();
			_produtoRepository = new Mock<IProdutoRepository>();
		}


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

    }

}

