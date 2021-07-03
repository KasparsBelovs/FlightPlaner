using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Models;

namespace FlightPlannerVS.Controllers
{
    
    public class CustomerApiController : ApiController
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        public CustomerApiController(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }

        [Route ("api/airports")]
        [HttpGet]
        public IHttpActionResult GetSearchAirport(string search)
        {
            var airports = _airportService.GetAirports(search);
            var airportResponseList = airports
                .Select(airport => _mapper.Map(airport, new AirportResponse()))
                .ToList();


            //var result = FlightStorage.SearchAllAirports(search);

            return airportResponseList.Count == 0 ? (IHttpActionResult) NotFound() : Ok(airportResponseList);
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
