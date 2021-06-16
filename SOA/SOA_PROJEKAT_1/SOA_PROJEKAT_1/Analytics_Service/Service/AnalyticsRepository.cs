using Analytics_Service.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics_Service.Service
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly IMongoCollection<SmartHomeTemperature> _smartHome;
        public readonly ILogger<IAnalyticsRepository> _logger;

        public AnalyticsRepository(ILogger<AnalyticsRepository> logger)
        {
            var client = new MongoClient("mongodb://dockercompose3724740770662679534_mongoAnalytics_1:27017");
             var database = client.GetDatabase("SmartHome");

            _smartHome = database.GetCollection<SmartHomeTemperature>("SmartHomeTemperature");
            this._logger = logger;
        }

        public async Task AddDataFromSensors(SmartHomeTemperature sht)
        {
            await _smartHome.InsertOneAsync(sht);
        }
    }
}
