
using System.Threading.Tasks;
using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AUTOGLASS_RodrigoLuz.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("{codigo}", Name = "GetRegistroPorCodigo")]
        public async Task<ActionResult> GetRegistroPorCodigo([FromRoute] int codigo)
        {
            var produto = await _produtoService.RecuperaProdutoPorCodigo(codigo);

            return Ok(produto);
        }

        [HttpGet]
        public async Task<ActionResult> ListaProdutos([FromQuery] FiltroDto filtro)
        {
            var produtos = await _produtoService.ListarProdutos(filtro);

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult> CadastraProduto([FromBody] ProdutoDto produto)
        {
            var retorno = await _produtoService.InsereProduto(produto);

            return Ok(retorno);
        }

        [HttpPut]
        public async Task<ActionResult> EditaProduto([FromBody] ProdutoDto produto)
        {
            var retorno = await _produtoService.EditaProduto(produto);

            return Ok(retorno);
        }

        [HttpDelete("{codigo}", Name = "Deletaproduto")]
        public async Task<ActionResult> DeletaProduto([FromRoute] int codigo)
        {
            var retorno = await _produtoService.ExcluiProduto(codigo);

            return Ok(retorno);
        }

    }
}

