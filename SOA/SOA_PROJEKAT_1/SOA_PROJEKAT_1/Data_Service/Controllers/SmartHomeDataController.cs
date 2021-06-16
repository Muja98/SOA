using Data_Service.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Service.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace Data_Service.Controllers
{
    [ApiController]
    [Route("api/smartHomeData")]
    public class SmartHomeDataController : Controller
    {
        private readonly ISmartHomeRepository _repository;
        private readonly IMessageService _messageService;

        public SmartHomeDataController(ISmartHomeRepository repository, IMessageService messageService)
        {
            _repository = repository;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDataFromSensor([FromBody] SmartHome sm)
        {
            await _repository.AddDataFromSensors(sm);
            string message = JsonSerializer.Serialize(sm);
            double temperature = sm.Temperature;
            _messageService.Enqueue(temperature.ToString());
            return Ok("Success. Data added");
        }

        [HttpGet]
        [Route("usage")]
        public async Task<IActionResult> GetByUse([FromQuery] float use, [FromQuery] string grSmUse)
        {
            var result = await _repository.GetByUse(use, grSmUse);
            return Ok(result);
        }

        [HttpGet]
        [Route("generated")]
        public async Task<IActionResult> GetByGen([FromQuery] float gen, [FromQuery] string grSmGen)
        {
            var result = await _repository.GetByGen(gen, grSmGen);
            return Ok(result);
        }
        [HttpGet]
        [Route("temperature")]
        public async Task<IActionResult> GetByTemp([FromQuery] float temp, [FromQuery] string grSmTemp)
        {
            var result = await _repository.GetByTemp(temp, grSmTemp);
            return Ok(result);
        }

        [HttpGet]
        [Route("allData")]
        public async Task<IActionResult> GetAll([FromQuery] int from, [FromQuery] int to)
        {
            var result = await _repository.GetAll(from, to);
            return Ok(result);
        }
    }
}
