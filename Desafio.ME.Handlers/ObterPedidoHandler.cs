using Desafio.ME.Database.Interfaces;
using Desafio.ME.DTOs;

namespace Desafio.ME.Handlers
{
    public class ObterPedidoHandler
    {
        private readonly IPedidoRepositorio _repositorio;

        public ObterPedidoHandler(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public PedidoDto Handle(string numeroPedido)
        {
            var pedido = _repositorio.ObterPorNumero(numeroPedido);
            if (pedido == null)
                return null;

            return new PedidoDto
            {
                Pedido = pedido.NumeroPedido,
                Itens = pedido.ItensDoPedido.Select(c => new ItemDto { Descricao = c.Descricao, PrecoUnitario = c.PrecoUnitario, Qtd = c.Quantidade }).ToList(),
                ValorTotal = pedido.ValorTotal
            };
        }
    }
}
