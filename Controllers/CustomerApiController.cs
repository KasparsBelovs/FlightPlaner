using System.Web.Http;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{
    
    public class CustomerApiController : ApiController
    {
        [Route ("api/airports")]
        [HttpGet]
        public IHttpActionResult GetSearchAirport(string search)
        {
            var result = FlightStorage.SearchAllAirports(search);

            return result.Count == 0 ? (IHttpActionResult) NotFound() : Ok(result);
        }

        [Route("api/flights/search")]
        [HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightRequest request)
        {
        
            if (FlightStorage.IsSearchFlightRequestInvalid(request))
            {
                return BadRequest();
            }

            return Ok(FlightStorage.GetSearchFlightRequestPage(request));
        }

        [Route("api/flights/{id}")]
        [HttpGet]
        public IHttpActionResult SearchFlightsById(int id)
        {
            var flight = FlightStorage.FindFlight(id);

            return flight == null ? (IHttpActionResult) NotFound() : Ok(flight);
        }
    }
}
