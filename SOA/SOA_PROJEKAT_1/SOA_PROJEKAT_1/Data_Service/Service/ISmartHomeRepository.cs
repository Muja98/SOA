using Data_Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.Service
{
    public interface ISmartHomeRepository
    {
        Task AddDataFromSensors(Object smartHome, string sensorType);
    }
}
