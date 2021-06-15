using System;
using System.Linq;
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
            return FlightStorage.FindFlight(id) == null ? (IHttpActionResult) NotFound() : Ok();
        }

        [Route("admin-api/flights")]
        [HttpPut]
        public IHttpActionResult PutFlight(FlightRequest request)
        {
            if (FlightStorage.IsFlightsPropNullOrEmpty(request))
            {
                return BadRequest("Properties can't be null or empty");
            }

            if (FlightStorage.IsArrivalTimeLessOrEqualToDepartureTime(request))
            {
                return BadRequest("Departure and Arrival Time error");
            }

            if (FlightStorage.IsFromAndToAirportsAreSame(request))
            {
                return BadRequest("City From and To are the same");
            }

            if (FlightStorage.IsFlightAlreadyInList(request))
            {
                return Conflict();
            }

            var flight = FlightStorage.MakeFlight(request);    

            FlightStorage.AddFlight(flight);

            return Created("", flight);
            
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
