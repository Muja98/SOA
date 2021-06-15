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
        [Route("setTimeInterval")]
        public async Task<IActionResult> setTimeInterval([FromBody] int interval)
        {
            string result = await _gatewayService.setTimeInterval(interval);
            return Ok(result);
        }

        [HttpGet]
        [Route("getTimeInterval")]
        public async Task<IActionResult> getTimeInterval()
        {
            string result = await _gatewayService.getTimeInterval();
            return Ok(result);
        }

        [HttpGet]
        [Route("getLastAction")]
        public async Task<IActionResult> getLastAction()
        {
            string result = await _gatewayService.getLastAction();
            return Ok(result);
        }

    }
}
