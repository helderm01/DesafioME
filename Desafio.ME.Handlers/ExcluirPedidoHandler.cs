using Desafio.ME.Database.Interfaces;

namespace Desafio.ME.Handlers
{

    public class ExcluirPedidoHandler
    {
        private readonly IPedidoRepositorio _repositorio;

        public ExcluirPedidoHandler(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Handle(string pedido)
        {
            if (!_repositorio.Any(c => c.NumeroPedido == pedido))
                throw new ArgumentException($"Pedido {pedido} não encontrado.");

            var pedidoParaExcluir = _repositorio.ObterPorNumero(pedido);
            _repositorio.Delete(pedidoParaExcluir);
        }
    }
}
