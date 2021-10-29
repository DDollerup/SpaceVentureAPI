using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAdventureAPI
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FlightTime { get; set; }
        public string Distance { get; set; }
        public double Price { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
