using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class AirportFromValidator : AirportValidator, IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return Validate(request.From);
        }
    }
}
