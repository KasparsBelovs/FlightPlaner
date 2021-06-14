using System.Web.Http;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{
    public class TestingApiController : ApiController
    {
        [Route("testing-api/clear")]
        [HttpPost]
        public IHttpActionResult Clear()
        { 
            FlightStorage.AllFlights.Clear();

            return Ok();
        }
    }
}
