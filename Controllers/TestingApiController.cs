using System.Web.Http;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Data;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{
    public class TestingApiController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;

        public TestingApiController(IFlightService flightService, IAirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [Route("testing-api/clear")]
        [HttpPost]
        public IHttpActionResult Clear()
        {
            _flightService.DeleteAllFlights();
            _airportService.DeleteAllAirports();

            return Ok();

            //lock (Locker.Lock)
            //{
            //    using (var ctx = new FlightPlannerDbContext())
            //    { 
            //        ctx.Flights.RemoveRange(ctx.Flights); 
            //        ctx.Airports.RemoveRange(ctx.Airports);
            //        ctx.SaveChanges();
            //    }

            //    return Ok();
            //}
        }
    }
}
