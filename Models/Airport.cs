using Newtonsoft.Json;

namespace FlightPlannerVS.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonProperty("airport")]
        public string AirportName { get; set; }

        public Airport(string country, string city, string airportName)
        {
            Country = country;
            City = city;
            AirportName = airportName;
        }
       
    }
}