using System.ComponentModel.DataAnnotations;

namespace Desafio.ME.DTOs
{
    public class AlterarStatusDto
    {
        [Required(ErrorMessage = "Status é obrigatório.")]
        public PedidoStatusDto Status { get; set; }

        [Required(ErrorMessage = "Pedido é obrigatório.")]
        public string Pedido { get; set; }

        public uint ItensAprovados { get; set; }

        public decimal ValorAprovado { get; set; }
    }
}
