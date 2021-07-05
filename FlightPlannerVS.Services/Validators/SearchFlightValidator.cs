using FlightPlannerVS.Core.Dto;

namespace FlightPlannerVS.Services.Validators
{
    public static class SearchFlightValidator
    {
        public static bool Validate(SearchFlightRequest request)
        {
            return request != null &&
                   request.To != request.From &&
                    !string.IsNullOrEmpty(request.To) &&
                    !string.IsNullOrEmpty(request.From) &&
                    !string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
