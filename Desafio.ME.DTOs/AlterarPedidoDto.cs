using System.ComponentModel.DataAnnotations;

namespace Desafio.ME.DTOs
{
    public class AlterarPedidoDto
    {
        [MaxLength(15, ErrorMessage = "NovoNumero deve conter no máximo {0} caracteres.")]
        public string NovoNumero { get; set; }

        public IList<ItemPedidoDto> NovosItens { get; set; } = new  List<ItemPedidoDto>();
    }
}
