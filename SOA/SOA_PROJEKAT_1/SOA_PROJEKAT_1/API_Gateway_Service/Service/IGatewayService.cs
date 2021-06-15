using API_Gateway_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway_Service.Service
{
    public interface IGatewayService
    {
        Task<string> AddDataFromSensors(SmartHome smartHome);
        Task<IEnumerable<SmartHome>> GetByUse(float use, string grSmUse);
        Task<IEnumerable<SmartHome>> GetByGen(float gen, string grSmGen);
        Task<IEnumerable<SmartHome>> GetByTemp(float temp, string grSmTemp);
        Task<IEnumerable<SmartHome>> GetAll(int from, int to);
        Task<string> setTimeInterval(int interval);
        Task<string> getTimeInterval();
        Task<string> getLastAction();
    }
}
