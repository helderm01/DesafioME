using Desafio.ME.Database.Interfaces;
using Desafio.ME.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Desafio.ME.Handlers.Testes
{
    public class CriarPedidoHandlerTestes : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public CriarPedidoHandlerTestes(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Deve_Ser_Possivel_Criar_Um_Pedido()
        {
            string numero = "123456";

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

            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);
        }

        [Fact]
        public void Nao_Deve_Ser_Possivel_Criar_Um_Pedido_Com_Numero_Duplicado()
        {
            string numero = "9999";

            var handler = _serviceProvider.GetService<CriarPedidoHandler>();
            var retornoCriacao = handler.Handle(new NovoPedidoDto()
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
            
            Assert.NotEmpty(retornoCriacao);
            Assert.Equal(numero, retornoCriacao);

            var retornoException = Assert.Throws<ArgumentException>(() => handler.Handle(new NovoPedidoDto()
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
            }));

            Assert.IsType<ArgumentException>(retornoException);
            Assert.Equal("Já existe um pedido com o número informado.", retornoException.Message);
        }
    }
}
