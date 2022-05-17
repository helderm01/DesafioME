using System;
using Xunit;

namespace Desafio.ME.Dominio.Testes
{
    public class PedidoTestes
    {
        private Pedido CriarPedidoEmEstadoValido() => new Pedido("112233");

        [Theory]
        [InlineData("Pedido-A")]
        [InlineData("Pedido-B")]
        [InlineData("123")]
        [InlineData("P")]
        public void Deve_Instanciar_Um_Pedido(string numero)
        {
            var pedido = new Pedido(numero);

            Assert.NotNull(pedido);
            Assert.Equal(numero, pedido.NumeroPedido);
            Assert.Equal(0, pedido.ValorTotal);
            Assert.Equal(0, pedido.QuantidadeTotal);
        }

        [Fact]
        public void Deve_Incluir_Itens_Ao_Pedido()
        {
            var pedido = CriarPedidoEmEstadoValido();

            pedido.IncluirItem("Item A", 5, 10);
            pedido.IncluirItem("Item B", 1, 5);
            pedido.IncluirItem("Item C", 2, 2.5m);

            Assert.NotEmpty(pedido.ItensDoPedido);
            Assert.Equal(3, pedido.ItensDoPedido.Count);
            Assert.Equal(8, pedido.QuantidadeTotal);
            Assert.Equal(60, pedido.ValorTotal);
        }

        [Fact]
        public void Nao_Deve_Incluir_Um_Item_Sem_Descricao()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var result = Assert.Throws<ArgumentException>(()=> pedido.IncluirItem("", 1, 5m));
            Assert.IsType<ArgumentException>(result);
            Assert.Equal("Descrição é obrigatória.", result.Message);
        }

        [Fact]
        public void Nao_Deve_Incluir_Um_Item_Com_Valor_Negativo()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var result = Assert.Throws<ArgumentException>(() => pedido.IncluirItem("Item A", 1, -5m));
            Assert.IsType<ArgumentException>(result);
            Assert.Equal("Preço unitário não pode ser menor que 0.", result.Message);
        }
        
        [Fact]
        public void Nao_Deve_Incluir_Um_Item_Com_Descricao_Muito_Grande()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var result = Assert.Throws<ArgumentException>(() => pedido.IncluirItem("012345678901234567890123456789012345678901234567891", 1, 5m));
            Assert.IsType<ArgumentException>(result);
            Assert.Equal("Descrição deve conter no máximo 50 caracteres.", result.Message);
        }

        [Fact]
        public void Nao_Deve_Incluir_Um_Item_Com_Quantidade_Invalida()
        {
            var pedido = CriarPedidoEmEstadoValido();

            var result = Assert.Throws<ArgumentException>(() => pedido.IncluirItem("Item A", 0, 5m));
            Assert.IsType<ArgumentException>(result);
            Assert.Equal("Quantidade deve ser maior que 0.", result.Message);
        }
    }
}
