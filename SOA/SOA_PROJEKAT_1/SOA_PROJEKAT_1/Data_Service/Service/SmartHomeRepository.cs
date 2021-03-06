using Data_Service.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Service.DatabaseSettings;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Data_Service.HubConfig;

namespace Data_Service.Service
{
    public class SmartHomeRepository : ISmartHomeRepository
    {
        private readonly IMongoCollection<SmartHome> _smartHome;
        public readonly ILogger<ISmartHomeRepository> _logger;
        private readonly IHubContext<DataFromSensorHub> _hub;
        public SmartHomeRepository(ISmartHomeMongoDatabaseSettings settings, ILogger<SmartHomeRepository> logger, IHubContext<DataFromSensorHub> hub)
        {
            var client = new MongoClient("mongodb://dockercompose4155219515517627736_mongo_1:27017"); 
             var database = client.GetDatabase("SmartHome");

            _smartHome = database.GetCollection<SmartHome>("Sensors");
            this._logger = logger;
            _hub = hub;
        }
        public async Task AddDataFromSensors(SmartHome smartHome)
        {
            await _smartHome.InsertOneAsync(smartHome);
            string groupName = "dataFromSensor";
            _ = _hub.Clients.Group(groupName).SendAsync("ReceiveMessage", smartHome);
        }

        public async Task<IEnumerable<SmartHome>> GetByUse(float use, string grSmUse)
        {
            var query = _smartHome.AsQueryable();

            if (!string.IsNullOrEmpty(grSmUse))
            {
                if(grSmUse == "gr")
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Use > use);
                else
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Use < use);
            }
            else
            {
                query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Use == use);
            }

            List<SmartHome> result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SmartHome>> GetByGen(float gen, string grSmGen)
        {
            var query = _smartHome.AsQueryable();

            if (!string.IsNullOrEmpty(grSmGen))
            {
                if (grSmGen == "gr")
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Gen > gen);
                else
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Gen < gen);
            }
            else
            {
                query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Gen == gen);
            }

            List<SmartHome> result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SmartHome>> GetByTemp(float temp, string grSmTemp)
        {
            var query = _smartHome.AsQueryable();

            if (!string.IsNullOrEmpty(grSmTemp))
            {
                if (grSmTemp == "gr")
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Temperature > temp);
                else
                    query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Temperature < temp);
            }
            else
            {
                query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.Where(n => n.Temperature == temp);
            }

            List<SmartHome> result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SmartHome>> GetAll(int from, int to)
        {
            var query = _smartHome.AsQueryable();
            query = (MongoDB.Driver.Linq.IMongoQueryable<SmartHome>)query.OrderByDescending(n => n.Id).Skip(from).Take(to - from);
            List<SmartHome> result = await query.ToListAsync();
            return result;
        }
    }
}
