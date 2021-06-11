using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FlightPlannerVS.Attributes;
using FlightPlannerVS.Models;
using Microsoft.Ajax.Utilities;

namespace FlightPlannerVS.Controllers
{
    
    public class CustomerApiController : ApiController
    {
        [Route ("api/airports")]
        [HttpGet]
        public IHttpActionResult GetSearchAirport(string search)
        {
            search = search.ToUpper().Trim();
            var result = new List<Airport>();

            foreach (var x in FlightStorage.AllFlights)
            {
                if (x.To.AirportName.ToUpper().Contains(search) ||
                    x.To.City.ToUpper().Contains(search) || 
                    x.To.Country.ToUpper().Contains(search))
                {
                    result.Add(x.To);
                }

                if (x.From.AirportName.ToUpper().Contains(search) ||
                    x.From.City.ToUpper().Contains(search) ||
                    x.From.Country.ToUpper().Contains(search))
                {
                    result.Add(x.From);
                }
            }
          
            return result.Count == 0 ? (IHttpActionResult)NotFound() : Ok(result);
        }

        [Route("api/flights/search")]
        [HttpGet]
        public IHttpActionResult SearchFlights(string from, string to, string date)
        {
            Flight result = null;
            foreach (var x in FlightStorage.AllFlights)
            {
                if (x.From.AirportName == from &&
                    x.To.AirportName == to &&
                    x.DepartureTime == date)
                {
                    return Ok(x);
                }
            }

            return null;
        }

        [Route("api/flights/{id}")]
        [HttpGet]
        public IHttpActionResult SearchFlightsById(int id)
        {
            var flight = FlightStorage.FindFlight(id);

            return flight == null ? (IHttpActionResult)NotFound() : Ok();
        }
    }
}
