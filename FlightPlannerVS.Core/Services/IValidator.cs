using FlightPlannerVS.Core.Dto;


namespace FlightPlannerVS.Core.Services
{
    public interface IValidator
    {
        bool Validate(FlightRequest request);
    }
}
