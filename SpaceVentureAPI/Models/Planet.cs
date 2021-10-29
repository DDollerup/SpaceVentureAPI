using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAdventureAPI
{
    public class Planet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
