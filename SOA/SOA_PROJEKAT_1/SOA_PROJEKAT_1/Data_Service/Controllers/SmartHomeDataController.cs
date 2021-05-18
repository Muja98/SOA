using Data_Service.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Service.Entities;
namespace Data_Service.Controllers
{
    [ApiController]
    [Route("api/smartHomeData")]
    public class SmartHomeDataController : Controller
    {
        private readonly ISmartHomeRepository _repository;

        public SmartHomeDataController(ISmartHomeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddDataFromSensor([FromBody] Object sm, [FromQuery] string sensorType)
        {
            await _repository.AddDataFromSensors(sm, sensorType);
            return Ok("Success. Data added");
        }

    }
}
