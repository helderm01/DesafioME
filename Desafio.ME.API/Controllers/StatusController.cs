using Desafio.ME.API.Dtos;
using Desafio.ME.DTOs;
using Desafio.ME.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.ME.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly AlterarStatusHandler _alterarStatusHandler;

        public StatusController(AlterarStatusHandler alterarStatusHandler)
        {
            _alterarStatusHandler = alterarStatusHandler;
        }

        [HttpPost]
        public IActionResult AlterarStatus(AlterarStatusDto alterarStatus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErroDto.Para(ModelState));

            try
            {
                var retorno = _alterarStatusHandler.Handle(alterarStatus);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ErroDto.Para(ex));
            }
        }
    }
}
