using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            var output = new Flight
            {
                ArrivalTime = newFlight.ArrivalTime,
                DepartureTime = newFlight.DepartureTime,
                From = newFlight.From,
                To = newFlight.To,
                Carrier = newFlight.Carrier
            };

            //if (FlightStorage.AllFlights.Count > 0 && FlightStorage.AllFlights.Any(x =>
            //    x.To == newFlight.To &&
            //    x.From == newFlight.From &&
            //    x.Carrier == newFlight.Carrier &&
            //    x.DepartureTime == newFlight.DepartureTime &&
            //    x.ArrivalTime == newFlight.ArrivalTime))
            //{
            //    return Conflict();
            //}

            FlightStorage.AddFlight(output);

            return Created("", output);
        }

        [Route("admin-api/flights")]
        [HttpDelete]
        public IHttpActionResult DeleteFlight(Flight flight)
        {
            return Ok();
        }
    }
}
