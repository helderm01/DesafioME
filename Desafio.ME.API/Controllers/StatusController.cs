using Desafio.ME.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.ME.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public StatusController()
        {

        }

        public IActionResult AlterarStatus(AlterarStatusDto alterarStatus)
        {
            return Ok();
        }
    }
}
