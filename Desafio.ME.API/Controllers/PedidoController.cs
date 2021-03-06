using Desafio.ME.API.Dtos;
using Desafio.ME.DTOs;
using Desafio.ME.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.ME.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly CriarPedidoHandler _criarPedidoHandler;
        private readonly ExcluirPedidoHandler _excluirPedidoHandler;
        private readonly ObterPedidoHandler _obterPedidoHandler;
        private readonly AlterarPedidoHandler _alterarPedidoHandler;

        public PedidoController(
            CriarPedidoHandler criarPedidoHandler,
            ExcluirPedidoHandler excluirPedidoHandler,
            ObterPedidoHandler obterPedidoHandler,
            AlterarPedidoHandler alterarPedidoHandler)
        {
            _criarPedidoHandler = criarPedidoHandler;
            _excluirPedidoHandler = excluirPedidoHandler;
            _obterPedidoHandler = obterPedidoHandler;
            _alterarPedidoHandler = alterarPedidoHandler;
        }

        [Route("{pedido}")]
        [HttpGet]
        public dynamic Get(string pedido)
        {
            var retorno = _obterPedidoHandler.Handle(pedido);
            if (retorno == null)
                return NotFound(ErroDto.ParaNotFound(pedido));

            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult CriarNovoPedido(NovoPedidoDto novoPedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErroDto.Para(ModelState));

            try
            {
                var numero = _criarPedidoHandler.Handle(novoPedido);

                return StatusCode(201, new { Pedido = numero });
            }
            catch (Exception ex)
            {
                return BadRequest(ErroDto.Para(ex));
            }
        }

        [HttpDelete]
        [Route("{pedido}")]
        public IActionResult RemoverPedido(string pedido)
        {
            try
            {
                _excluirPedidoHandler.Handle(pedido);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErroDto.Para(ex));
            }
        }

        [HttpPut]
        [Route("{pedido}")]
        public IActionResult AlterarPedido(string pedido, AlterarPedidoDto alterarPedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErroDto.Para(ModelState));

            try
            {
                _alterarPedidoHandler.Handle(pedido, alterarPedido);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErroDto.Para(ex));
            }
        }
    }
}
