﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor_Device_Service.StaticClasses
{
    public static class SmartHomeStaticData
    {
        public static int timeInterval { set; get; } = 24;
        public static int sensorType { set; get; } = 1;//1-all sensors; 2-electricity consumption; 3-other sensors except electricity;s

    }
}
