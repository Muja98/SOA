using Command_Service.StaticClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Service.Controllers
{
    [ApiController]
    [Route("api/command")]
    public class CommandController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> setCurrentAction([FromBody] string action)
        {
            CurrentAction.currentAction = action;
            return Ok("Action setted: " + CurrentAction.currentAction);
        }
    }
}
