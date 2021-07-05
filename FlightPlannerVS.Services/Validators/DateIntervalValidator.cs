using System;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class DateIntervalValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            var arrivalDate = DateTime.Parse(request.ArrivalTime);
            var departureDate = DateTime.Parse(request.DepartureTime);

            return arrivalDate > departureDate;
        }
    }
}
