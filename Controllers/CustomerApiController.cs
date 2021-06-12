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
        [HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightRequest request)
        {
            if (request == null ||
                request.To == request.From ||
                string.IsNullOrEmpty(request.To) ||
                string.IsNullOrEmpty(request.From) ||
                string.IsNullOrEmpty(request.DepartureDate)
            )
            {
                return BadRequest();
            }

            var page = new PageResult();

            foreach (var flight in FlightStorage.AllFlights)
            {
                if (flight.From.AirportName == request.From &&
                    flight.To.AirportName == request.To &&
                    flight.DepartureTime == request.DepartureDate)
                {
                    page.items.Add(flight);
                }
            }

            return Ok(page);
        }

        [Route("api/flights/{id}")]
        [HttpGet]
        public IHttpActionResult SearchFlightsById(int id)
        {
            var flight = FlightStorage.FindFlight(id);

            return flight == null ? (IHttpActionResult)NotFound() : Ok(flight);
        }
    }
}
