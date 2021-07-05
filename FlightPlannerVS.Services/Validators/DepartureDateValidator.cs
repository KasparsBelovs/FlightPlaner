using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class DepartureDateValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}
