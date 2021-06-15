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
        public async Task<IActionResult> AddDataFromSensor([FromBody] SiddhiEvent result)
        {
            //Console.WriteLine("Ovo je rezultat" + result.Event.temperature + " - " + result.Event.averageTemperature);
            if (result.Event.averageTemperature < 38.0)
            {
                _messageService.Enqueue("0");
                string response = await _messageService.sendActionToCommandService("Interval setted to 5");
                return Ok(response);
            }
            else
            {
                _messageService.Enqueue("1");
                string response = await _messageService.sendActionToCommandService("Interval setted to 3");
                return Ok(response);
            }
        }
    }
}
