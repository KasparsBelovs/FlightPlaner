using System.Web.Http;
using FlightPlannerVS.DbContext;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{
    public class TestingApiController : ApiController
    {
        [Route("testing-api/clear")]
        [HttpPost]
        public IHttpActionResult Clear()
        { 
            //FlightStorage.AllFlights.Clear();
            using (var ctx = new FlightPlannerDbContext())
            {
                ctx.Flights.SqlQuery("DELETE FROM Flights;");
                ctx.Flights.SqlQuery("DELETE FROM Airports;");
            }

            return Ok();
        }
    }
}
