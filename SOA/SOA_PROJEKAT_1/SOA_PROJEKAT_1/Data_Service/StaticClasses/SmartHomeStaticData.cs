using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.StaticClasses
{
    public static class SmartHomeStaticData
    {
        public static int timeInterval { set; get; } = 5;
        public static int sensorType { set; get; } = 1;//1-all sensors; 2-electricity consumption; 3-other sensors except electricity;s

    }
}
