namespace Desafio.ME.Dominio.Enum
{
    [Flags]
    public enum PedidoStatus
    {
        APROVADO,
        APROVADO_VALOR_A_MENOR,
        APROVADO_VALOR_A_MAIOR,
        APROVADO_QTD_A_MAIOR,
        APROVADO_QTD_A_MENOR,
        REPROVADO,
        CODIGO_PEDIDO_INVALIDO
    }
}
