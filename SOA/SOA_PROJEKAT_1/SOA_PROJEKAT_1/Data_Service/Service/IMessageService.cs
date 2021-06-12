using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.Service
{
    public interface IMessageService
    {
        bool Enqueue(string message);
    }
}
