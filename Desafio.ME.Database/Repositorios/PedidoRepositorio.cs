using Desafio.ME.Database.Context;
using Desafio.ME.Database.Interfaces;
using Desafio.ME.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Desafio.ME.Database.Repositorios
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(MEContext context)
            : base(context)
        {
        }

        public Pedido ObterPorNumero(string numero)
            => Context.Pedidos.Include(c=>c.ItensDoPedido).SingleOrDefault(c => c.NumeroPedido == numero);
    }
}
