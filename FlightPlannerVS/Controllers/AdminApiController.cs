using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using FlightPlannerVS.Attributes;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Controllers
{
   
    [BasicAuthentication]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class AdminApiController : ApiController
    {
        private static readonly object locker = new object();
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
        [HttpGet]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);
            var output = _mapper.Map(flight, new FlightResponse());
            return flight == null ? (IHttpActionResult) NotFound() : Ok(output);
        }

        [Route("admin-api/flights")]
        [HttpPut]
        public IHttpActionResult PutFlight(FlightRequest request)
        {
            lock (locker)
            {
                if (!_validators.All(x => x.Validate(request)))
                    return BadRequest();

                if (_flightService.IsFlightInDb(request))
                    return Conflict();

                var flight = _mapper.Map(request, new Flight());
                _flightService.Create(flight);

                return Created("", _mapper.Map(flight, new FlightResponse()));
            }
        }

        [Route("admin-api/flights/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteFlight(int id)
        {
            lock (locker)
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
}
