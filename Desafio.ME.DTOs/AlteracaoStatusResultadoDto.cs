using Desafio.ME.Dominio.Enum;

namespace Desafio.ME.DTOs
{
    public class AlteracaoStatusResultadoDto
    {
        public string Pedido { get; set; }
        public PedidoStatus[] Status { get; set; }
    }
}
