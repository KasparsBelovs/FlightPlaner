using System.Collections.Generic;
using FlightPlannerVS.Core.Models;

namespace FlightPlannerVS.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        void DeleteAllAirports();
        IEnumerable<Airport> GetAirports(string search);
    }
}
