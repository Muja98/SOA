using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.Model
{
    public class SmartHomeSensorsExceptElectricityDto
    {
        public int Time { get; set; }
        public double Temperature { get; set; }
        public string Icon { get; set; }
        public double Humidity { get; set; }
        public double Visibility { get; set; }
        public string Summary { get; set; }
        public double ApparentTemperature { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string CloudCover { get; set; }
        public double WindBearing { get; set; }
        public double PrecipIntensity { get; set; }
        public double DewPoint { get; set; }
        public double PrecipProbability { get; set; }
    }
}
