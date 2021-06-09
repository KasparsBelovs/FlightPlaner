using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace FlightPlannerVS.Models
{
    public static class FlightStorage
    {
        public static List<Flight> AllFlights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(Flight newFlight)
        {
        //    if (AllFlights.Any(x => 
        //        x.To == newFlight.To &&
        //        x.From == newFlight.From &&
        //        x.Carrier == newFlight.Carrier &&
        //        x.DepartureTime == newFlight.DepartureTime &&
        //        x.ArrivalTime == newFlight.ArrivalTime))
        //    {
        //        throw new Exception("fdsfhsafks");
        //    }
            
            newFlight.Id = _id;
            _id++;
            AllFlights.Add(newFlight);

            return newFlight;
        }

        public static Flight FindFlight(int id)
        {
            return AllFlights.FirstOrDefault(x => x.Id == id);
        }
    }
}