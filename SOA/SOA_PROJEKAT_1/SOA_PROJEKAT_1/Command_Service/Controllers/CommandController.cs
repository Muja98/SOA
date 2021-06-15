using Command_Service.Service;
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
        private readonly ICommandService _commandService;

        public CommandController(ICommandService commandService)
        {
            this._commandService = commandService;
        }

        [HttpPost]
        [Route("setTimeInterval")]
        public async Task<IActionResult> changeTimeInterval([FromBody] int interval)
        {
            string result = await this._commandService.setTimeInterval(interval);
            CurrentAction.currentAction = result;
            return Ok(CurrentAction.currentAction);
        }

        [HttpGet]
        [Route("getTimeInterval")]
        public async Task<IActionResult> getTimeInterval()
        {
            string result = await this._commandService.getTimeInterval();
            return Ok(result);
        }

        [HttpGet]
        [Route("getLastAction")]
        public async Task<IActionResult> getLastAction()
        { 
            return Ok(StaticClasses.CurrentAction.currentAction);
        }
    }
}
