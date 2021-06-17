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
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                { 
                    ctx.Flights.RemoveRange(ctx.Flights); 
                    ctx.Airports.RemoveRange(ctx.Airports);
                    ctx.SaveChanges();
                }

                return Ok();
            }
            //FlightStorage.AllFlights.Clear();
        }
    }
}
