using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class AirportCodesValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return request.From.Airport.ToLower().Trim() != request.To.Airport.ToLower().Trim();
        }
    }
}
