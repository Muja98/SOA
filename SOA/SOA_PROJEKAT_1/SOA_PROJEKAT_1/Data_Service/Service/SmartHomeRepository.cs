using Data_Service.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Service.DatabaseSettings;

namespace Data_Service.Service
{
    public class SmartHomeRepository : ISmartHomeRepository
    {
        private readonly IMongoCollection<SmartHome> _smartHome;

        public SmartHomeRepository(ISmartHomeMongoDatabaseSettings settings)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SmartHome");

            _smartHome = database.GetCollection<SmartHome>("Sensors");
        }
        public async Task AddDataFromSensors(SmartHome smartHome)
        {
            await _smartHome.InsertOneAsync(smartHome);
        }
    }
}
