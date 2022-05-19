using Desafio.ME.Database.Interfaces;
using Desafio.ME.Dominio.Enum;
using Desafio.ME.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Desafio.ME.Handlers.Testes
{
    public class AlterarStatusHandlerTestes : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;
        private const string NumeroPedido = "1234";
        public AlterarStatusHandlerTestes(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private void CriarUmNovoPedido(string numero)
        {
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
        }

        [Fact]
        public void Deve_Aprovar_Um_Pedido()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 2,
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 20m
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.APROVADO, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Reprovar_Um_Pedido()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.REPROVADO,
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.REPROVADO, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Aprovar_Um_Pedido_Valor_Maior()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 2,
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 30m
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MAIOR, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Aprovar_Um_Pedido_Valor_Menor()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 2,
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 10m
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.APROVADO_VALOR_A_MENOR, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Aprovar_Um_Pedido_Qtd_Maior()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 3,
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 20m
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MAIOR, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Aprovar_Um_Pedido_Qtd_Menor()
        {
            CriarUmNovoPedido(NumeroPedido);
            var repositorio = _serviceProvider.GetService<IPedidoRepositorio>();
            var pedido = repositorio.ObterPorNumero(NumeroPedido);
            Assert.Equal(1, pedido.ItensDoPedido.Count);
            Assert.Equal(20, pedido.ValorTotal);
            Assert.Equal(2, pedido.QuantidadeTotal);

            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 1,
                Pedido = NumeroPedido,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 20m
            });

            Assert.Equal(NumeroPedido, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.APROVADO_QTD_A_MENOR, result.Status[0]);
            repositorio.Delete(pedido);
        }

        [Fact]
        public void Deve_Retornar_Codigo_Pedido_Invalido()
        {
            var numero = "0";
            var handler = _serviceProvider.GetService<AlterarStatusHandler>();
            var result = handler.Handle(new AlterarStatusDto
            {
                ItensAprovados = 1,
                Pedido = numero,
                Status = PedidoStatusDto.APROVADO,
                ValorAprovado = 20m
            });

            Assert.Equal(numero, result.Pedido);
            Assert.Equal(1, result.Status.Length);
            Assert.Equal(PedidoStatus.CODIGO_PEDIDO_INVALIDO, result.Status[0]);
        }
    }
}
