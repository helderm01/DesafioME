using Desafio.ME.Dominio.Enum;

namespace Desafio.ME.Dominio
{
    public class PedidoAprovacao
    {

        public PedidoAprovacao(Pedido pedido, ICollection<PedidoStatus> status, uint qtdItens, decimal valor)
        {
            AtribuirStatus(status);
            AtribuirItensAprovados(qtdItens);
            AtribuirPedido(pedido);
            AtribuirValorAprovado(valor);
        }

        public ICollection<PedidoStatus> Status { get; private set; }
        public uint ItensAprovados { get; private set; }
        public decimal ValorAprovado { get; private set; }
        public Pedido Pedido { get; private set; }


        public void AtribuirStatus(ICollection<PedidoStatus> status) => Status = status;
        public void AtribuirPedido(Pedido pedido) => Pedido = pedido;
        public void AtribuirItensAprovados(uint itensAprovados)
        {
            if (itensAprovados == 0 && Status.Contains(PedidoStatus.REPROVADO))
                throw new ArgumentException("Para informar uma quantidade 0 o status do pedido deve ser REPROVADO.");

            ItensAprovados = itensAprovados;
        }
        public void AtribuirValorAprovado(decimal valorAprovado)
        {
            if (valorAprovado < 0)
                throw new ArgumentException("Valor aprovado deve ser maior ou igual a 0.");

            if (valorAprovado == 0 && Status.Contains(PedidoStatus.REPROVADO))
                throw new ArgumentException("Para informar um valor 0 o status do pedido deve ser REPROVADO.");

            ValorAprovado = valorAprovado;
        }
    }
}
