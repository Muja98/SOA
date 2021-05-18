using Data_Service.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Service.DatabaseSettings;
using Microsoft.Extensions.Logging;

namespace Data_Service.Service
{
    public class SmartHomeRepository : ISmartHomeRepository
    {
        private readonly IMongoCollection<Object> _smartHome;
        public readonly ILogger<ISmartHomeRepository> _logger;

        public SmartHomeRepository(ISmartHomeMongoDatabaseSettings settings, ILogger<SmartHomeRepository> logger)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SmartHome");

            _smartHome = database.GetCollection<Object>("Sensors");
            this._logger = logger;
        }
        public async Task AddDataFromSensors(Object smartHome, string sensorType)
        {
            _logger.LogInformation(sensorType);
            int sensor = int.Parse(sensorType);
            if (sensor == 1)
            {
                await _smartHome.InsertOneAsync((SmartHome)smartHome);
            }
            else if (sensor == 2)
            {
                await _smartHome.InsertOneAsync((SmartHomeElectricityDto)smartHome);
            }
            else {
                await _smartHome.InsertOneAsync((SmartHomeSensorsExceptElectricityDto)smartHome);
            }
        }
    }
}
