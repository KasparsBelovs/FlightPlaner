using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlannerVS.Models
{
    public static class AirportStorage
    {
        public static List<Airport> AllAirports = new List<Airport>();

        public static Airport FindAirport(string airportName)
        {
            return AllAirports.FirstOrDefault(x => x.AirportName == airportName);
        }
    }
}