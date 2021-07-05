using System.ComponentModel.DataAnnotations;

namespace FlightPlannerVS.Core.Models
{
    public class Flight : Entity
    {
        public virtual Airport From { get; set; }
        public virtual Airport To { get; set; }
        [ConcurrencyCheck]
        public string Carrier { get; set; }
        [ConcurrencyCheck]
        public string DepartureTime { get; set; }
        [ConcurrencyCheck]
        public string ArrivalTime { get; set; }
    }
}