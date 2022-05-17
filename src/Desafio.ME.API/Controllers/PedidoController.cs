using Microsoft.AspNetCore.Mvc;

namespace Desafio.ME.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        public dynamic Get()
        {
            return new
            {
                Pedido = "123456",
                Itens = new dynamic[]
                {
                    new
                    {
                        Descricao = "Item A",
                        PrecoUnitario = 10,
                        Qtd = 1
                    },
                    new
                    {
                        Descricao = "Item B",
                        PrecoUnitario = 5,
                        Qtd = 2
                    }
                }
            };
        }
    }
}
