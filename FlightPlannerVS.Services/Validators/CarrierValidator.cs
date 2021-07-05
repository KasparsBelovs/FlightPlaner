using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class CarrierValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.Carrier);
        }
    }
}
