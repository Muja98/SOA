using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Data_Service.Entities
{
    public class SmartHome
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Time { get; set; }
        public double Use { get; set; }
        public double Gen { get; set; }
        public double HouseOverall { get; set; }
        public double Dishwasher { get; set; }
        public double Furnace1 { get; set; }
        public double Furnace2 { get; set; }
        public double HomeOffice { get; set; }
        public double Fridge { get; set; }
        public double WineCellar { get; set; }
        public double GarageDoor { get; set; }
        public double Kitchen1 { get; set; }
        public double Kitchen2 { get; set; }
        public double Kitchen3 { get; set; }
        public double Barn { get; set; }
        public double Well { get; set; }
        public double Microwave { get; set; }
        public double LivingRoom { get; set; }
        public double Solar { get; set; }
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
