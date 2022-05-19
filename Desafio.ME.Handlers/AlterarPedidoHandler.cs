using Desafio.ME.Database.Interfaces;
using Desafio.ME.DTOs;

namespace Desafio.ME.Handlers
{
    public class AlterarPedidoHandler
    {
        private readonly IPedidoRepositorio _repositorio;

        public AlterarPedidoHandler(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Handle(string numeroPedido, AlterarPedidoDto dto)
        {
            if (!_repositorio.Any(c => c.NumeroPedido == numeroPedido))
                throw new ArgumentException($"Pedido {numeroPedido} não encontrado.");

            var pedido = _repositorio.ObterPorNumero(numeroPedido);
            
            if (!string.IsNullOrWhiteSpace(dto.NovoNumero))
                pedido.AtribuirNumeroPedido(dto.NovoNumero);

            foreach(var novoItem in dto.NovosItens)
            {
                pedido.IncluirItem(novoItem.Descricao, novoItem.Qtd, novoItem.PrecoUnitario);
            }

            _repositorio.Update(pedido);
        }
    }
}
