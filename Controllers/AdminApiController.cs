using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlannerVS.Attributes;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Models;


namespace FlightPlannerVS.Controllers
{

    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService, IEnumerable<IValidator> validators, IMapper mapper)
        {
            _flightService = flightService;
            _validators = validators;
            _mapper = mapper;
        }

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);
            return flight == null ? (IHttpActionResult) NotFound() : Ok();
        }

        [Route("admin-api/flights")]
        [HttpPut]
        public IHttpActionResult PutFlight(FlightRequest request)
        {
            if (!_validators.All(x => x.Validate(request)))
                return BadRequest();
            //if (FlightStorage.IsFlightsPropNullOrEmpty(request))
            //{
            //    return BadRequest("Properties can't be null or empty");
            //}

            //if (FlightStorage.IsArrivalTimeLessOrEqualToDepartureTime(request))
            //{
            //    return BadRequest("Departure and Arrival Time error");
            //}

            //if (FlightStorage.IsFromAndToAirportsAreSame(request))
            //{
            //    return BadRequest("City From and To are the same");
            //}

            //if (FlightStorage.IsFlightAlreadyInList(request))
            //{
            //    return Conflict();
            //}

            var flight = _mapper.Map(request, new Flight());
            _flightService.Create(flight);

            var response = _mapper.Map(flight, new FlightResponse());
           // FlightStorage.AddFlight(flight);

            return Created("", response);
            
        }

        [Route("admin-api/flights/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);

            return Ok();
        }
    }
}
