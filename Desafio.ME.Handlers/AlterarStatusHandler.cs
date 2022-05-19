using Desafio.ME.Database.Interfaces;
using Desafio.ME.Dominio;
using Desafio.ME.DTOs;

namespace Desafio.ME.Handlers
{
    public class AlterarStatusHandler
    {
        private readonly IPedidoRepositorio _repositorio;

        public AlterarStatusHandler(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public AlteracaoStatusResultadoDto Handle(AlterarStatusDto dto)
        {
            if (!_repositorio.Any(c => c.NumeroPedido == dto.Pedido))
            {
                return new AlteracaoStatusResultadoDto
                {
                    Pedido = dto.Pedido,
                    Status = new Dominio.Enum.PedidoStatus[] { Dominio.Enum.PedidoStatus.CODIGO_PEDIDO_INVALIDO }
                };
            }

            var pedido = _repositorio.ObterPorNumero(dto.Pedido);

            PedidoAprovacao aprovacao;
            if (dto.Status == PedidoStatusDto.APROVADO)
                aprovacao = pedido.AprovarPedido(dto.ItensAprovados, dto.ValorAprovado);
            else
                aprovacao = pedido.ReprovarPedido();

            return new AlteracaoStatusResultadoDto
            {
                Pedido = pedido.NumeroPedido,
                Status = aprovacao.Status.ToArray()
            };
        }
    }
}
