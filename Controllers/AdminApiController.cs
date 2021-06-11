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
        public IHttpActionResult PutFlight(AddFlightRequest newFlight)
        {
            if (newFlight.To == null ||
                string.IsNullOrEmpty(newFlight.To?.AirportName) ||
                string.IsNullOrEmpty(newFlight.To?.City) ||
                string.IsNullOrEmpty(newFlight.To?.Country) ||
                newFlight.From == null ||
                string.IsNullOrEmpty(newFlight.From?.AirportName) ||
                string.IsNullOrEmpty(newFlight.From?.City) ||
                string.IsNullOrEmpty(newFlight.From?.Country) ||
                string.IsNullOrEmpty(newFlight.Carrier) ||
                string.IsNullOrEmpty(newFlight.ArrivalTime) ||
                string.IsNullOrEmpty(newFlight.DepartureTime))
            {
                return BadRequest("Check properties, they can't be null or empty");
            }

            if (DateTime.Compare(
                DateTime.Parse(newFlight.ArrivalTime),
                DateTime.Parse(newFlight.DepartureTime)) <= 0)
            {
                return BadRequest("Check Departure and Arrival Time");
            }
            
            if (newFlight.To.AirportName.ToLower().Trim() == newFlight.From.AirportName.ToLower().Trim())
            {
                return BadRequest("City From and To are the same");
            }

            if (
                (FlightStorage.AllFlights.Any(x =>
                    x.From.AirportName == newFlight.From.AirportName &&
                    x.From.City == newFlight.From.City &&
                    x.From.Country == newFlight.From.Country &&
                    x.To.AirportName == newFlight.To.AirportName &&
                    x.To.City == newFlight.To.City &&
                    x.To.Country == newFlight.To.Country &&
                    x.Carrier == newFlight.Carrier &&
                    x.ArrivalTime == newFlight.ArrivalTime &&
                    x.DepartureTime == newFlight.DepartureTime
                )))
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
    }
}
