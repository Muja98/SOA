using API_Gateway_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway_Service.Controllers
{
    [ApiController]
    [Route("api/commandGateway")]
    public class CommandGatewayController : Controller
    {
        private readonly IGatewayService _gatewayService;

        public CommandGatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpPost]
        public async Task<IActionResult> setCurrentAction([FromBody] string action)
        {
            string result = await _gatewayService.setCurrentCommand(action);
            return Ok(result);
        }

    }
}
