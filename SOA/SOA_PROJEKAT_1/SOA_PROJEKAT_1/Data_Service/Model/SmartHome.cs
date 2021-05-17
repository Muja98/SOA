using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Service.Model
{
    public class SmartHome
    {
        public int Time { get; set; }
        public float Use { get; set; }
        public float Gen { get; set; }
        public float HouseOverall { get; set; }
        public float Dishwasher { get; set; }
        public float Furnace1 { get; set; }
        public float Furnace2 { get; set; }
        public float HomeOffice { get; set; }
        public float Fridge { get; set; }
        public float WineCellar { get; set; }
        public float GarageDoor  { get; set; }
        public float Kitchen1 { get; set; }
        public float Kitchen2 { get; set; }
        public float Kitchen3 { get; set; }
        public float Barn { get; set; }
        public float Well { get; set; }
        public float Microwave { get; set; }
        public float LivingRoom { get; set; }
        public float Solar { get; set; }
        public float Temperature { get; set; }
        public string Icon { get; set; }
        public float Humidity { get; set; }
        public float Visibility { get; set; }
        public string Summary { get; set; }
        public float ApparentTemperature { get; set; }
        public float Pressure { get; set; }
        public float WindSpeed { get; set; }
        public string CloudCover { get; set; }
        public float WindBearing { get; set; }
        public float PrecipIntensity { get; set; }
        public float dewPoint { get; set; }
        public float PrecipProbability { get; set; }
    }
}
