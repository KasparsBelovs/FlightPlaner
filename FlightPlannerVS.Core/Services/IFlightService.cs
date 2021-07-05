using System.Collections.Generic;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;

namespace FlightPlannerVS.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);
        void DeleteAllFlights();
        bool IsFlightInDb(FlightRequest request);
        List<Flight> GetSearchFlightRequestPage(SearchFlightRequest request);
        Flight FindFlight(int id);
    }
}
