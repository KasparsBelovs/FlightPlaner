using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlannerVS.Attributes;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Models;
using FlightPlannerVS.Services.Validators;


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

            if (_flightService.IsFlightInDb(request))
                return Conflict();

            var flight = _mapper.Map(request, new Flight());
            _flightService.Create(flight);

            return Created("", _mapper.Map(flight, new FlightResponse()));
        }

        [Route("admin-api/flights/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetById(id);
            if (flight != null)
            {
                _flightService.Delete(flight);
            }

            return Ok();
        }
    }
}
