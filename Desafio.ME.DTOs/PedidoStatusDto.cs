using System.Text.Json.Serialization;

namespace Desafio.ME.DTOs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PedidoStatusDto
    {
        APROVADO,
        REPROVADO
    }
}
