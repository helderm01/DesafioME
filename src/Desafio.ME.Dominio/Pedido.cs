using Desafio.ME.Dominio.Enum;

namespace Desafio.ME.Dominio
{
    public class Pedido : EntidadeBase
    {
        const uint TAMANHO_NUMERO = 15;

        public Pedido()
        {
            ItensDoPedido = new List<PedidoItem>();
        }

        public string NumeroPedido { get; private set; }
        public ICollection<PedidoItem> ItensDoPedido { get; private set; }
        public long QuantidadeTotal => ItensDoPedido.Sum(item => item.Quantidade);
        public decimal ValorTotal => ItensDoPedido.Sum(item => item.ValorTotal);

        public void AtribuirNumeroPedido(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Número do pedido é obrigatório.");

            if (numero.Length > TAMANHO_NUMERO)
                throw new ArgumentException($"Número do pedido deve conter no máximo {TAMANHO_NUMERO} caracteres.");

            NumeroPedido = numero;
        }
        public void IncluirItem(string descricao, uint quantidade, decimal preco)
        {
            ItensDoPedido.Add(new PedidoItem(descricao, quantidade, preco));
        }
        public PedidoAprovacao AprovarPedido(PedidoStatus status, uint itensAprovados, decimal valorAprovado)
        {
            if (status != PedidoStatus.APROVADO)
                throw new ArgumentException("Para aprovar o pedido o Status deve ser APROVADO.");

            ICollection<PedidoStatus> statusAprovacao = new List<PedidoStatus>();

            if (itensAprovados > QuantidadeTotal)
                statusAprovacao.Add(PedidoStatus.APROVADO_QTD_A_MAIOR);

            if (itensAprovados < QuantidadeTotal)
                statusAprovacao.Add(PedidoStatus.APROVADO_QTD_A_MENOR);

            if (valorAprovado > ValorTotal)
                statusAprovacao.Add(PedidoStatus.APROVADO_VALOR_A_MAIOR);

            if (valorAprovado < ValorTotal)
                statusAprovacao.Add(PedidoStatus.APROVADO_VALOR_A_MENOR);

            if (valorAprovado == ValorTotal && itensAprovados == QuantidadeTotal)
                statusAprovacao.Add(PedidoStatus.APROVADO);

            return new PedidoAprovacao(this, statusAprovacao, itensAprovados, valorAprovado);
        }

        public PedidoAprovacao ReprovarPedido(PedidoStatus status)
        {
            if (status != PedidoStatus.REPROVADO)
                throw new ArgumentException("Para reprovar o pedido o Status deve ser REPROVADO.");

            ICollection<PedidoStatus> statusAprovacao = new List<PedidoStatus>();
            statusAprovacao.Add(PedidoStatus.REPROVADO);

            return new PedidoAprovacao(this, statusAprovacao, 0, 0);
        }
    }
}