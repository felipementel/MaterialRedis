using Aplicacao.API.Controllers.Base;
using Aplicacao.Application.DTOs;
using Aplicacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aplicacao.API.Controllers.v1
{
    /// <summary>
    /// Orders
    /// </summary>
    //[Authorize("Bearer")]
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController : BaseController<OrderDTO, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appService"></param>
        public OrdersController(IOrderAppService appService) : base(appService) { }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
