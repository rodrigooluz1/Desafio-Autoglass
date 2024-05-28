using AUTOGLASS_RodrigoLuz.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AUTOGLASS_RodrigoLuz.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<ProdutoResponseDto> RecuperaProdutoPorCodigo(int codigo);

        Task<ListaProdutoResponseDto> ListarProdutos(FiltroDto filtro);

        Task<ProdutoResponseDto> InsereProduto(ProdutoDto produto);

        Task<BaseResponseDto> EditaProduto(ProdutoDto produto);

        Task<BaseResponseDto> ExcluiProduto(int codigo);
    }
}

