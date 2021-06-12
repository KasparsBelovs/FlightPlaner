using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FlightPlannerVS.Attributes;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{

    [BasicAuthentication]
    public class AdminApiController : ApiController
    {

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = FlightStorage.FindFlight(id);
            return flight == null ? (IHttpActionResult) NotFound() : Ok();
        }

        [Route("admin-api/flights")]
        [HttpPut]
        public IHttpActionResult PutFlight(FlightRequest newFlight)
        {
            if(IsFlightsPropNullOrEmpty(newFlight))
            {
                return BadRequest("Check properties, they can't be null or empty");
            }

            if (IsArrivalTimeLMoreThanDepartureTime(newFlight))
            {
                return BadRequest("Check Departure and Arrival Time");
            }
            
            if (IsFromAndToAirportsAreSame(newFlight))
            {
                return BadRequest("City From and To are the same");
            }

            if (IsFlightAlreadyInList(newFlight))
            {
                return Conflict();
            }

            var output = new Flight
            {
                ArrivalTime = newFlight.ArrivalTime,
                DepartureTime = newFlight.DepartureTime,
                From = newFlight.From,
                To = newFlight.To,
                Carrier = newFlight.Carrier
            };

            FlightStorage.AddFlight(output);

            return Created("", output);
        }

        [Route("admin-api/flights/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);

            return Ok();
        }

        public bool IsFlightsPropNullOrEmpty(FlightRequest newFlight)
        {
            return newFlight.To == null ||
                   string.IsNullOrEmpty(newFlight.To?.AirportName) ||
                   string.IsNullOrEmpty(newFlight.To?.City) ||
                   string.IsNullOrEmpty(newFlight.To?.Country) ||
                   newFlight.From == null ||
                   string.IsNullOrEmpty(newFlight.From?.AirportName) ||
                   string.IsNullOrEmpty(newFlight.From?.City) ||
                   string.IsNullOrEmpty(newFlight.From?.Country) ||
                   string.IsNullOrEmpty(newFlight.Carrier) ||
                   string.IsNullOrEmpty(newFlight.ArrivalTime) ||
                   string.IsNullOrEmpty(newFlight.DepartureTime);
        }

        public bool IsArrivalTimeLMoreThanDepartureTime(FlightRequest newFlight)
        {
            return DateTime.Compare(
                DateTime.Parse(newFlight.ArrivalTime),
                DateTime.Parse(newFlight.DepartureTime)) <= 0;
        }

        public bool IsFromAndToAirportsAreSame(FlightRequest newFlight)
        {
            return newFlight.To.AirportName.ToLower().Trim() == newFlight.From.AirportName.ToLower().Trim();
        }

        public bool IsFlightAlreadyInList(FlightRequest newFlight)
        {
            return (FlightStorage.AllFlights.Any(x =>
                x.From.AirportName == newFlight.From.AirportName &&
                x.From.City == newFlight.From.City &&
                x.From.Country == newFlight.From.Country &&
                x.To.AirportName == newFlight.To.AirportName &&
                x.To.City == newFlight.To.City &&
                x.To.Country == newFlight.To.Country &&
                x.Carrier == newFlight.Carrier &&
                x.ArrivalTime == newFlight.ArrivalTime &&
                x.DepartureTime == newFlight.DepartureTime
            ));
        }
    }
}
