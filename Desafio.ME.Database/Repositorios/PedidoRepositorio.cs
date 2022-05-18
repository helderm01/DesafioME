using Desafio.ME.Database.Context;
using Desafio.ME.Database.Interfaces;
using Desafio.ME.Dominio;

namespace Desafio.ME.Database.Repositorios
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(MEContext context)
            : base(context)
        {
        }
    }
}
