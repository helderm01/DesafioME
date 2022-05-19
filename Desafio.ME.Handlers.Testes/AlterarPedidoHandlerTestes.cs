using Desafio.ME.Database.Interfaces;
using Desafio.ME.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Desafio.ME.Handlers.Testes
{
    public class AlterarPedidoHandlerTestes : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public AlterarPedidoHandlerTestes(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Deve_Alterar_Um_Pedido()
        {
            string numero = "654321";

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

            string novoNumero = "12";
            var handlerAlterar = _serviceProvider.GetService<AlterarPedidoHandler>();
            handlerAlterar.Handle(numero, new AlterarPedidoDto
            {
                NovoNumero = novoNumero
            });

            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(novoNumero);

            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);
        }

        [Fact]
        public void Deve_Incluir_Novos_Itens_Ao_Pedido()
        {
            string numero = "4321";

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

            var handlerAlterar = _serviceProvider.GetService<AlterarPedidoHandler>();
            handlerAlterar.Handle(numero, new AlterarPedidoDto
            {
                NovosItens = new[]
                {
                    new ItemPedidoDto
                    {
                        Descricao = "Item B",
                        PrecoUnitario = 5m,
                        Qtd = 1u
                    }
                }
            });

            var pedidoAlterado = repositorio.ObterPorNumero(numero);

            Assert.NotNull(pedidoAlterado);
            Assert.Equal(2, pedidoAlterado.ItensDoPedido.Count);
            Assert.Equal(25, pedidoAlterado.ValorTotal);
            Assert.Equal(3, pedidoAlterado.QuantidadeTotal);
        }

        [Fact]
        public void Nao_Deve_Alterar_Um_Pedido_Inexistente()
        {
            var handlerAlterar = _serviceProvider.GetService<AlterarPedidoHandler>();
            var retornoException = Assert.Throws<ArgumentException>(()=> handlerAlterar.Handle("1", new AlterarPedidoDto
            {
                NovoNumero = "12"
            }));

            Assert.IsType<ArgumentException>(retornoException);
            Assert.Equal("Pedido 1 não encontrado.", retornoException.Message);
        }
    }
}
