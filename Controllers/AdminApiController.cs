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

            if (FlightStorage.AllFlights.Contains(output))
            {
                //throw new Exception("This flight is already registered.");
                return BadRequest("This flight is already registered.");
            }

            FlightStorage.AddFlight(output);

            return Created("", output);
        }

    }
}
