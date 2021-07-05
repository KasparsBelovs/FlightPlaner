using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Data;

namespace FlightPlannerVS.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return Query().Include(x => x.From)
                .Include(x => x.To)
                .SingleOrDefault(x => x.Id == id);
        }

        public void DeleteAllFlights()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
        }

        public bool IsFlightInDb(FlightRequest request)
        {
            return _context.Flights.Any(x =>
                x.From.AirportName == request.From.Airport &&
                x.From.City == request.From.City &&
                x.From.Country == request.From.Country &&
                x.To.AirportName == request.To.Airport &&
                x.To.City == request.To.City &&
                x.To.Country == request.To.Country &&
                x.Carrier == request.Carrier &&
                x.ArrivalTime == request.ArrivalTime &&
                x.DepartureTime == request.DepartureTime
            );
        }

        public List<Flight> GetSearchFlightRequestPage(SearchFlightRequest request)
        {
            return _context.Flights.Where(flight => 
                flight.From.AirportName == request.From && 
                flight.To.AirportName == request.To && 
                flight.DepartureTime.Substring(0, 10) == request.DepartureDate)
                .ToList();
        }

        public Flight FindFlight(int id)
        {
            return _context.Flights.Find(id);
        }
    }
}
