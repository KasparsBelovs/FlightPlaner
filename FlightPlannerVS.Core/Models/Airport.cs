using System.ComponentModel.DataAnnotations;

namespace FlightPlannerVS.Core.Models
{
    public class Airport : Entity
    {
        [ConcurrencyCheck]
        public string Country { get; set; }
        [ConcurrencyCheck]
        public string City { get; set; }
        [ConcurrencyCheck]
        public string AirportName { get; set; }
    }
}