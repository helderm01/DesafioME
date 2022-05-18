using System.ComponentModel.DataAnnotations;

namespace Desafio.ME.DTOs
{
    public class NovoPedidoDto
    {
        [Required(ErrorMessage = "Pedido obrigatório.")]
        [MaxLength(15, ErrorMessage = "Pedido deve conter no máximo {0} caracteres.")]
        public string Pedido { get; set; }
        
        [Required(ErrorMessage = "Necessário informar ao menos um item.")]
        public IList<ItemPedidoDto> Itens { get; set; } = new List<ItemPedidoDto>();
    }
}
