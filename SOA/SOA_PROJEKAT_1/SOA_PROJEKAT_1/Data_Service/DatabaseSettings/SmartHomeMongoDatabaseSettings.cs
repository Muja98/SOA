using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.DatabaseSettings
{
    public class SmartHomeMongoDatabaseSettings : ISmartHomeMongoDatabaseSettings
    {
        public string SensorsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
