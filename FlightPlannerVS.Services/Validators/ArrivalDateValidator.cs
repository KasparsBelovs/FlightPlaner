using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;

namespace FlightPlannerVS.Services.Validators
{
    public class ArrivalDateValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.ArrivalTime);
        }
    }
}
