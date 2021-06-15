using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway_Service.Models
{
    public class SmartHomeElectricityDto
    {
        public int Time { get; set; }
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
    }
}
