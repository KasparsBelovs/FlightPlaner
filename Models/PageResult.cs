using System.Collections.Generic;
using FlightPlannerVS.Core.Models;

namespace FlightPlannerVS.Models
{
    public class PageResult<T> where T : Flight
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }

        public List<T> Items = new List<T>();

    }
}