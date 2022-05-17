using System;
using Xunit;

namespace Desafio.ME.Dominio.Testes
{
    public class PedidoItemTestes
    {
        private PedidoItem ConstroiPedidoBase()
        {
            return new PedidoItem("Item A", 1, 5);
        }


        [Theory]
        [InlineData("Item A", 1, 10, 10)]
        [InlineData("Item B", 2, 20, 40)]
        [InlineData("Item C", 5, 50, 250)]
        public void Deve_Instanciar_Um_Item(string descricao, uint qtde, decimal preco, decimal totalEsperado)
        {
            var pedidoItem = new PedidoItem(descricao, qtde, preco);

            Assert.Equal(descricao, pedidoItem.Descricao);
            Assert.Equal(qtde, pedidoItem.Quantidade);
            Assert.Equal(preco, pedidoItem.PrecoUnitario);
            Assert.Equal(totalEsperado, pedidoItem.ValorTotal);
        }
        
        [Fact]
        public void Nao_Deve_Atribuir_Vazio_Na_Descricao()
        {
            var item = ConstroiPedidoBase();

            var result = Assert.Throws<ArgumentException>(() => item.DefinirDescricao(string.Empty));
            Assert.Equal("Descrição é obrigatória.", result.Message);

        }

        [Fact]
        public void Nao_Deve_Atribuir_Descricao_Muito_Grande()
        {
            var item = ConstroiPedidoBase();

            string novaDescricao = "012345678901234567890123456789012345678901234567891";
            var result = Assert.Throws<ArgumentException>(() => item.DefinirDescricao(novaDescricao));
            Assert.Equal("Descrição deve conter no máximo 50 caracteres.", result.Message);
        }

        [Fact]
        public void Nao_Deve_Atribuir_Valor_Negativo()
        {
            var item = ConstroiPedidoBase();

            var result = Assert.Throws<ArgumentException>(() => item.DefinirPrecoUnitario(-10m));
            Assert.Equal("Preço unitário não pode ser menor que 0.", result.Message);
        }
    }
}