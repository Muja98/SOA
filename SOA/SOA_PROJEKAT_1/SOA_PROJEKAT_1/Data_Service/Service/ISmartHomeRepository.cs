using Data_Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.Service
{
    public interface ISmartHomeRepository
    {
        Task AddDataFromSensors(SmartHome smartHome);
        Task<IEnumerable<SmartHome>> GetAll(int from, int to);
        Task<IEnumerable<SmartHome>> GetByUse(float use, string grSmUse);
        Task<IEnumerable<SmartHome>> GetByGen(float gen, string grSmGen);
        Task<IEnumerable<SmartHome>> GetByTemp(float temp, string grSmTemp);
    }
}
