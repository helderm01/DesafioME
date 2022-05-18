using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Desafio.ME.API.Dtos
{
    public class ErroDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ErroDto[] Details { get; set; }

        private ErroDto(string code, string message, ErroDto[] details)
        {
            Code = code;
            Message = message;
            Details = details;
        }

        public static ErroDto ParaNotFound(string busca)
        {
            return new ErroDto(code: StatusCodes.Status404NotFound.ToString(),
                               message: $"Not Found ({busca})",
                               details: null);
        }

        public static ErroDto Para(Exception exception)
        {
            if (exception == null)
                return null;

            return new ErroDto(code: exception.HResult.ToString(),
                               message: exception.Message,
                               details: null);
        }

        public static ErroDto Para(ModelStateDictionary modelState)
        {
            if (modelState == null)
                return null;

            var details = modelState.Values.SelectMany(x => x.Errors)
                                           .Select(x => new ErroDto("400", x.ErrorMessage, null))
                                           .ToArray();

            return BadRequestError(details);
        }

        private static ErroDto BadRequestError(ErroDto[] details)
        {
            return new ErroDto(code: "400",
                               message: "O conteúdo enviado na requisição é inválido.",
                               details: details);
        }
    }
}
