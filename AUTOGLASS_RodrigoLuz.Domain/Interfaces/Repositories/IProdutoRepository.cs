using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AUTOGLASS_RodrigoLuz.Domain.Interfaces.Repositories
{
	public interface IProdutoRepository
	{
        Task<Produto> RecuperaProdutoPorCodigo(int codigo);

        Task<List<Produto>> ListarProdutos(FiltroDto filtro);

        Task<Produto> InsereProduto(Produto produto);

        Task<bool> AtualizaProduto(Produto produto);
    }
}

