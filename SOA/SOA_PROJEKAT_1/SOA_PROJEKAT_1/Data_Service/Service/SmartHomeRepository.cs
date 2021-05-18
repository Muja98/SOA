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
        private readonly IMongoCollection<SmartHome> _smartHome;
        public readonly ILogger<ISmartHomeRepository> _logger;

        public SmartHomeRepository(ISmartHomeMongoDatabaseSettings settings, ILogger<SmartHomeRepository> logger)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SmartHome");

            _smartHome = database.GetCollection<SmartHome>("Sensors");
            this._logger = logger;
        }
        public async Task AddDataFromSensors(SmartHome smartHome)
        {
            await _smartHome.InsertOneAsync(smartHome);
        }
    }
}
