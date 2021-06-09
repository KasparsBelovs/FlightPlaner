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
    public class CustomerApiController : ApiController
    {
        [Route("api/airports/{airport}")]
        [HttpGet]
        public IHttpActionResult SearchAirport(string airport)
        {
            return Ok();
        }

        [Route("api/flights")]
        [HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightRequest flightRequest)
        {
            return Ok();
        }

        [Route("api/flights/{id}")]
        [HttpGet]
        public IHttpActionResult SearchFlightsById(int id)
        {
            return Ok();
        }
    }
}
