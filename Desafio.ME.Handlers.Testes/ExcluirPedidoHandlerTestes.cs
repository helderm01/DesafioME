using Desafio.ME.Database.Interfaces;
using Desafio.ME.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Desafio.ME.Handlers.Testes
{
    public class ExcluirPedidoHandlerTestes : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public ExcluirPedidoHandlerTestes(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Deve_Excluir_Um_Pedido()
        {
            string numero = "312";

            var handler = _serviceProvider.GetService<CriarPedidoHandler>();
            var retorno = handler.Handle(new NovoPedidoDto()
            {
                Pedido = numero,
                Itens = new[] {
                    new ItemPedidoDto
                    {
                        Descricao = "Item A",
                        PrecoUnitario = 10m,
                        Qtd = 2u
                    }
                }
            });

            Assert.NotEmpty(retorno);
            Assert.Equal(numero, retorno);

            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(numero);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            repositorio.Delete(pedido);
            var pedidoAposExclusao = repositorio.ObterPorNumero(numero);

            Assert.Null(pedidoAposExclusao);
        }

        [Fact]
        public void Nao_Deve_Excluir_Um_Pedido_Inexistente()
        {
            var handler = _serviceProvider.GetService<ExcluirPedidoHandler>();
            var retornoException = Assert.Throws<ArgumentException>(() => handler.Handle("1"));

            Assert.IsType<ArgumentException>(retornoException);
            Assert.Equal("Pedido 1 não encontrado.", retornoException.Message);
        }
    }
}
