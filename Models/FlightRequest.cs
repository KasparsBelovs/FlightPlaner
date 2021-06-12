using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightPlannerVS.Controllers;

namespace FlightPlannerVS.Models
{
    public class FlightRequest
    {
        public Airport From { get; set; }
        public Airport To { get; set; }

        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

    }
}