using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AUTOGLASS_RodrigoLuz.Infra.Repositories
{
	public class ProdutoRepository : IProdutoRepository
	{
        private readonly Context _context;

		public ProdutoRepository(Context context)
		{
            _context = context;
		}

        public async Task<Produto> RecuperaProdutoPorCodigo(int codigo)
        {
            return await _context.Set<Produto>()
                .Include(p => p.Fornecedor)
                .Where(p => p.Id == codigo && p.Situacao == true).FirstOrDefaultAsync();
        }

        public async Task<List<Produto>> ListarProdutos(FiltroDto filtro)
        {
            return await _context.Set<Produto>()
                    .Include(p => p.Fornecedor)
                    .Where(p => (p.Situacao == true)
                        && (filtro.CodigoProduto == null || p.Id == filtro.CodigoProduto)
                        && (filtro.Descricao == null || p.Descricao.Contains(filtro.Descricao))
                        && (filtro.DataFabricacao == null || p.DataFabricacao == filtro.DataFabricacao)
                        && (filtro.DataValidade == null || p.DataValidade == filtro.DataValidade)
                        && (filtro.CodigoFornecedor == null || p.IdFornecedor == filtro.CodigoFornecedor)
                        && (filtro.DescricaoFornecedor == null || p.Fornecedor.DescricaoFornecedor == filtro.DescricaoFornecedor)
                        && (filtro.CNPJFornecedor == null || p.Fornecedor.CNPJFornecedor == filtro.CNPJFornecedor)
                    )
                    .Skip(filtro.TamanhoPagina * (filtro.NumeroPagina - 1))
                    .Take(filtro.TamanhoPagina)
                    .ToListAsync();
        }

        public async Task<Produto> InsereProduto(Produto produto)
        {
            _context.Set<Produto>().Add(produto);
            await _context.SaveChangesAsync();

            return produto;
            
        }

        public async Task<bool> AtualizaProduto(Produto produto)
        {
            _context.Set<Produto>().Update(produto);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}

