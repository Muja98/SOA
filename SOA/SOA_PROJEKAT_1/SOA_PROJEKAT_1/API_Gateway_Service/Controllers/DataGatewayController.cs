using API_Gateway_Service.Models;
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
    [Route("api/dataGateway")]
    public class DataGatewayController : Controller
    {
        private readonly IGatewayService _gatewayService;

        public DataGatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDataFromSensors([FromBody] SmartHome sm)
        {
            string result = await _gatewayService.AddDataFromSensors(sm);
            return Ok(result);
        }

        [HttpGet]
        [Route("usage")]
        public async Task<IActionResult> GetByUse([FromQuery] float use, [FromQuery] string grSmUse)
        {
            var result = await _gatewayService.GetByUse(use, grSmUse);
            return Ok(result);
        }

        [HttpGet]
        [Route("generated")]
        public async Task<IActionResult> GetByGen([FromQuery] float gen, [FromQuery] string grSmGen)
        {
            var result = await _gatewayService.GetByGen(gen, grSmGen);
            return Ok(result);
        }

        [HttpGet]
        [Route("temperature")]
        public async Task<IActionResult> GetByTemp([FromQuery] float temp, [FromQuery] string grSmTemp)
        {
            var result = await _gatewayService.GetByTemp(temp, grSmTemp);
            return Ok(result);
        }

        [HttpGet]
        [Route("allData")]
        public async Task<IActionResult> GetAll([FromQuery] int from, [FromQuery] int to)
        {
            var result = await _gatewayService.GetAll(from, to);
            return Ok(result);
        }

    }
}
