using System.ComponentModel.DataAnnotations;

namespace Desafio.ME.DTOs
{
    public class ItemPedidoDto
    {
        [Required(ErrorMessage = "Descrição obrigatória.")]
        [MaxLength(50, ErrorMessage = "Descricao deve conter no máximo {0} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço Unitário obrigatório.")]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Preço Unitário deve ser maior que 0.")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade obrigatória.")]
        public uint Qtd { get; set; }
    }
}
