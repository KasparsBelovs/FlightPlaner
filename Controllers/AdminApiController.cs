using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightPlannerVS.Attributes;

namespace FlightPlannerVS.Controllers
{
    [Route ("admin-api/flights/{id}")]
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        public IEnumerable<string> GetFlights(int id)
        {
            return new string[] {"value1", "value2"};
        }
    }
}
