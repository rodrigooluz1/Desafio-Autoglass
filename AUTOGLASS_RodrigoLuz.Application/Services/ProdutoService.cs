using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AUTOGLASS_RodrigoLuz.Domain.Constants;
using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Repositories;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Services;
using AutoMapper;

namespace AUTOGLASS_RodrigoLuz.Application.Services
{
	public class ProdutoService: IProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

		public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
		{
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoResponseDto> RecuperaProdutoPorCodigo(int codigo)
        {
            try
            {
                var produto = await _produtoRepository.RecuperaProdutoPorCodigo(codigo);

                if(produto == null)
                    return new ProdutoResponseDto()
                    {
                        Code = ResponseStatusCodes.NotFound,
                        Message = "Produto não encontrado"
                    };

                var retornoDto = _mapper.Map<ProdutoDto>(produto);

                return new ProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Success,
                    Produto = retornoDto
                };
            }
            catch(Exception ex)
            {
                return new ProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Error,
                    Message = ex.Message
                };
            }
        }

        public async Task<ListaProdutoResponseDto> ListarProdutos(FiltroDto filtro)
        {
            try
            {
                var listaProdutos = await _produtoRepository.ListarProdutos(filtro);

                if (listaProdutos == null || listaProdutos.Count == 0)
                    return new ListaProdutoResponseDto()
                    {
                        Code = ResponseStatusCodes.NotFound,
                        Message = "Nenhum produto na lista"
                    };

                var produtosDto = _mapper.Map<List<ProdutoDto>>(listaProdutos);

                return new ListaProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Success,
                    Produtos = produtosDto
                };
            }
            catch (Exception ex)
            {
                return new ListaProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Error,
                    Message = ex.Message
                };
            }
        }

        public async Task<ProdutoResponseDto> InsereProduto(ProdutoDto produto)
        {
            try
            {
                var produtoEntity = new Produto(produto.CodigoProduto, produto.Descricao, produto.Situacao, produto.DataFabricacao, produto.DataValidade, produto.CodigoFornecedor);

                var novoProduto = await _produtoRepository.InsereProduto(produtoEntity);

                if (novoProduto == null)
                    throw new Exception("Erro ao cadastrar produto.");

                var retornoDto = _mapper.Map<ProdutoDto>(novoProduto);

                return new ProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Success,
                    Message = "Produto cadastrado com sucesso.",
                    Produto = retornoDto
                };

            }
            catch(Exception ex)
            {
                return new ProdutoResponseDto()
                {
                    Code = ResponseStatusCodes.Error,
                    Message = ex.Message
                };
            }
            
        }


        public async Task<BaseResponseDto> EditaProduto(ProdutoDto produto)
        {
            try
            {
                var produtoEntity = new Produto(produto.CodigoProduto, produto.Descricao, produto.Situacao, produto.DataFabricacao, produto.DataValidade, produto.CodigoFornecedor);

                var produtoAtualizado = await _produtoRepository.AtualizaProduto(produtoEntity);

                if (produtoAtualizado)
                {
                    return new BaseResponseDto()
                    {
                        Code = ResponseStatusCodes.Success,
                        Message = "Produto editado com sucesso"
                    };
                }
                else
                    throw new Exception("Erro ao editar produto.");

            }
            catch (Exception ex)
            {
                return new BaseResponseDto()
                {
                    Code = ResponseStatusCodes.Error,
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponseDto> ExcluiProduto(int codigo)
        {
            try
            {
                var produtoEntity = await _produtoRepository.RecuperaProdutoPorCodigo(codigo);

                if (produtoEntity == null)
                    return new BaseResponseDto()
                    {
                        Code = ResponseStatusCodes.NotFound,
                        Message = "Produto não encontrado"
                    };

                produtoEntity.Situacao = false; //Altera Situacao para false e efetua exclusao lógica

                var produtoDeletado = await _produtoRepository.AtualizaProduto(produtoEntity);

                if (produtoDeletado)
                {
                    return new BaseResponseDto()
                    {
                        Code = ResponseStatusCodes.Success,
                        Message = "Produto deletado"
                    };
                }else
                    throw new Exception("Erro ao deletar produto.");

            }
            catch (Exception ex)
            {
                return new BaseResponseDto()
                {
                    Code = ResponseStatusCodes.Error,
                    Message = ex.Message
                };
            }
        }
        
    }
}

