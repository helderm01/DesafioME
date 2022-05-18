namespace Desafio.ME.DTOs
{
    public class PedidoDto
    {
        public string Pedido { get; set; }
        public IList<ItemDto> Itens { get; set; } = new List<ItemDto>();
        public decimal ValorTotal { get; set; }
    }
}
