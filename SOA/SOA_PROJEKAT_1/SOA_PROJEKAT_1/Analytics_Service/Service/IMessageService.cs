using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics_Service.Service
{
    public interface IMessageService
    {
        bool Enqueue(string message);
        Task<string> sendActionToCommandService(string action);
    }
}
