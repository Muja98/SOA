using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor_Device_Service.Controllers
{
    [ApiController]
    [Route("api/smartHome")]
    public class SmartHomeController : Controller
    {
        public SmartHomeController() { }

        [HttpPut]
        [Route("interval")]
        public IActionResult setTimeInterval([FromBody] int timeInterval)
        {
            StaticClasses.SmartHomeStaticData.timeInterval = timeInterval;

            return Ok("Success");
        }

        [HttpGet]
        [Route("sensorType")]
        public IActionResult getSensorType()
        {
            string sensorTypeSeted = "";
            switch (StaticClasses.SmartHomeStaticData.sensorType)
            {
                case 1: sensorTypeSeted = "All sensors"; break;
                case 2: sensorTypeSeted = "Only electricity sensors"; break;
                case 3: sensorTypeSeted = "Other sensors except electricity"; break;
                default: break;
            }

            return Ok(sensorTypeSeted);
        }

        [HttpGet]
        [Route("interval")]
        public IActionResult getTimeInterval()
        {
            int timeInterval = StaticClasses.SmartHomeStaticData.timeInterval;
            return Ok(timeInterval);
        }

        [HttpPost]
        [Route("sensorType")]
        public IActionResult setSensorType([FromBody] int sensorType)
        {
            if (sensorType < 1 || sensorType > 3)
            {
                return BadRequest("Sensor type must bee 1,2 or 3");
            }
            StaticClasses.SmartHomeStaticData.sensorType = sensorType;
            string sensorTypeSeted = "";
            switch (sensorType)
            {
                case 1: sensorTypeSeted = "Success. All sensors"; break;
                case 2: sensorTypeSeted = "Success. Only electricity sensors"; break;
                case 3: sensorTypeSeted = "Success. Other sensors except electricity"; break;
                default: break;
            }
            return Ok(sensorTypeSeted);
        }



    }
}
