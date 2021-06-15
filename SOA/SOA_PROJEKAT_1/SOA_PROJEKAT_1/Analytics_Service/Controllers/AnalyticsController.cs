using Analytics_Service.Models;
using Analytics_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Analytics_Service.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : Controller
    {
        private readonly IMessageService _messageService;
        public AnalyticsController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeData([FromBody] SiddhiEvent result)
        {
            if (result.Event.averageTemperature < 38.0)
            {
                _messageService.Enqueue("0");
                return Ok("Time interval has not changed");
            }
            else
            {
                _messageService.Enqueue("1");
                return Ok("Time interval has changed ");
            }
        }
    }
}
