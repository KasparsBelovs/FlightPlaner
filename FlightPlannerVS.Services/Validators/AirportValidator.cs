using FlightPlannerVS.Core.Dto;

namespace FlightPlannerVS.Services.Validators
{
    public class AirportValidator
    {
        protected bool Validate(AirportRequest airport)
        {
            return !string.IsNullOrEmpty(airport?.City) &&
                   !string.IsNullOrEmpty(airport?.Country) &&
                   !string.IsNullOrEmpty(airport?.Airport);
        }
    }
}
