﻿using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Services.Validators;

namespace FlightPlannerVS.Controllers
{
    public class CustomerApiController : ApiController
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        public CustomerApiController(IAirportService airportService, IFlightService flightService, IMapper mapper)
        {
            _airportService = airportService;
            _flightService = flightService;
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

            return airportResponseList.Count == 0 ? (IHttpActionResult) NotFound() : Ok(airportResponseList);
        }

        [Route("api/flights/search")]
        [HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightRequest request)
        {
            if (!SearchFlightValidator.Validate(request))
                return BadRequest();
      
            var flightList = _flightService.GetSearchFlightRequestPage(request);
            var flightResponseList = flightList.Select(flight => _mapper.Map(flight, new FlightResponse())).ToList();
            var page = new PageResultResponse()
            {
                TotalItems = flightResponseList.Count,
                Items = flightResponseList
            };

            return Ok(page);
        }

        [Route("api/flights/{id}")]
        [HttpGet]
        public IHttpActionResult SearchFlightsById(int id)
        {
            var flight = _flightService.FindFlight(id);

            return flight == null ? (IHttpActionResult) NotFound() : Ok(_mapper.Map(flight, new FlightResponse()));
        }
    }
}
