using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlannerVS.Models
{
    public class PageResult
    {
        public int page { get; set; }
        public int totalItems { get; set; }

        public List<Flight> items = new List<Flight>();

    }
}