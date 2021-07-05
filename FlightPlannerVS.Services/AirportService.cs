using System.Collections.Generic;
using System.Linq;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Data;

namespace FlightPlannerVS.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void DeleteAllAirports()
        {
           
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
            
        }

        public IEnumerable<Airport> GetAirports(string search)
        {
            search = search.ToUpper().Trim();

            return _context.Airports.Where(airport => 
                airport.AirportName.ToUpper().Contains(search) || 
                airport.City.ToUpper().Contains(search) || 
                airport.Country.ToUpper().Contains(search))
                .ToList();
        }
    }
}
