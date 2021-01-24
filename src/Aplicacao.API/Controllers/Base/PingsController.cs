using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacao.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PingsController : Controller
    {
        /// <summary>
        /// Action para testes de disponibilidade. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok("Pong");
    }
}