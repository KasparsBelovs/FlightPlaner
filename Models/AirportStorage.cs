using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlannerVS.Models
{
    public static class AirportStorage
    {
        public static List<Airport> AllAirports = new List<Airport>()
        {
            new Airport("Latvia", "Riga", "RIX"),
            new Airport("Russia", "Moscow", "DME"),
            new Airport("United Arab Emirates", "Dubai", "DXB"),
            new Airport("Sweden", "Stockholm", "ARN")
        };

    }
}