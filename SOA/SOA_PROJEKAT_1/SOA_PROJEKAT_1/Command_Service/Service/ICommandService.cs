using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Service.Service
{
    public interface ICommandService
    {
        Task<string> setTimeInterval(int interval);
        Task<string> getTimeInterval();
    }
}
