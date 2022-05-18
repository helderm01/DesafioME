using Desafio.ME.Database.Interfaces;
using Desafio.ME.Dominio;
using Desafio.ME.DTOs;

namespace Desafio.ME.Handlers
{
    public class CriarPedidoHandler
    {
        private readonly IPedidoRepositorio _repositorio;

        public CriarPedidoHandler(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public string Handle(NovoPedidoDto dto)
        {
            if (_repositorio.Any(c => c.NumeroPedido == dto.Pedido))
                throw new ArgumentException("Já existe um pedido com o número informado.");

            var novoPedido = new Pedido(dto.Pedido);
            foreach(var item in dto.Itens)
            {
                novoPedido.IncluirItem(item.Descricao, item.Qtd, item.PrecoUnitario);
            }

            _repositorio.Post(novoPedido);

            return novoPedido.NumeroPedido;
        }
    }
}
