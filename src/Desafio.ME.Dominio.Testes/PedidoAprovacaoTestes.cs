using Desafio.ME.Dominio.Enum;
using Xunit;

namespace Desafio.ME.Dominio.Testes
{
    public class PedidoAprovacaoTestes
    {
        private Pedido CriarPedidoEmEstadoValido()
        {
            var pedido = new Pedido("123");
            pedido.IncluirItem("Item A", 5, 10);
            pedido.IncluirItem("Item B", 1, 5);
            pedido.IncluirItem("Item C", 2, 2.5m);

            return pedido;
        }

        [Fact]
        public void Aprovar_Pedido()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(8, 60m);
            Assert.Equal(1, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO, aprovacao.Status[0]);
            Assert.Equal(8u, aprovacao.ItensAprovados);
            Assert.Equal(60m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Valor_Menor()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(8, 40m);
            Assert.Equal(1, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MENOR, aprovacao.Status[0]);
            Assert.Equal(8u, aprovacao.ItensAprovados);
            Assert.Equal(40m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Valor_Maior()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(8, 100m);
            Assert.Equal(1, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MAIOR, aprovacao.Status[0]);
            Assert.Equal(8u, aprovacao.ItensAprovados);
            Assert.Equal(100m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Quantidade_Menor()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(2, 60m);
            Assert.Equal(1, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MENOR, aprovacao.Status[0]);
            Assert.Equal(2u, aprovacao.ItensAprovados);
            Assert.Equal(60m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Quantidade_Maior()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(10, 60m);
            Assert.Equal(1, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MAIOR, aprovacao.Status[0]);
            Assert.Equal(10u, aprovacao.ItensAprovados);
            Assert.Equal(60m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Quantidade_e_Valor_Maior()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(10, 100m);
            Assert.Equal(2, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MAIOR, aprovacao.Status[0]);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MAIOR, aprovacao.Status[1]);
            Assert.Equal(10u, aprovacao.ItensAprovados);
            Assert.Equal(100m, aprovacao.ValorAprovado);
        }

        [Fact]
        public void Aprovar_Pedido_Quantidade_e_Valor_Menor()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var aprovacao = pedido.AprovarPedido(5, 30m);
            Assert.Equal(2, aprovacao.Status.Count);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MENOR, aprovacao.Status[0]);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MENOR, aprovacao.Status[1]);
            Assert.Equal(5u, aprovacao.ItensAprovados);
            Assert.Equal(30m, aprovacao.ValorAprovado);
        }
    }
}
