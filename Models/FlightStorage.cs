using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using FlightPlannerVS.DbContext;

namespace FlightPlannerVS.Models
{
    public static class FlightStorage
    {
        public static List<Flight> AllFlights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(Flight flight)
        {
            lock (Locker.Lock)
            {
                flight.Id = _id;
                _id++;
               //AllFlights.Add(flight);
               using (var ctx = new FlightPlannerDbContext())
               {
                   ctx.Flights.Add(flight);
                   ctx.SaveChanges();
               }

               return flight;
            }
        }

        public static Flight FindFlight(int id)
        {
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    return ctx.Flights.Include(x => x.To).Include(x => x.From).SingleOrDefault(x => x.Id == id);
                }
                   // return AllFlights.FirstOrDefault(x => x.Id == id);
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    var flight = FindFlight(id);
                    if (flight != null)
                    {
                        ctx.Flights.Attach(flight);
                        ctx.Flights.Remove(flight);
                        ctx.SaveChanges();
                    }
                }

                //AllFlights.Remove(FindFlight(id));
            }
        }

        public static Flight MakeFlight(FlightRequest request)
        {
            lock (Locker.Lock)
            {
                return new Flight
                {
                    ArrivalTime = request.ArrivalTime,
                    DepartureTime = request.DepartureTime,
                    From = request.From,
                    To = request.To,
                    Carrier = request.Carrier
                };
            }
        }

        public static List<Airport> SearchAllAirports(string search)
        {
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    var output = new List<Airport>();
                    search = search.ToUpper().Trim();

                    foreach (var x in ctx.Flights.Include(x => x.To).Include(x => x.From))
                    {
                        if (x.To.AirportName.ToUpper().Contains(search) ||
                            x.To.City.ToUpper().Contains(search) ||
                            x.To.Country.ToUpper().Contains(search))
                        {
                            output.Add(x.To);
                        }

                        if (x.From.AirportName.ToUpper().Contains(search) ||
                            x.From.City.ToUpper().Contains(search) ||
                            x.From.Country.ToUpper().Contains(search))
                        {
                            output.Add(x.From);
                        }
                    }

                    return output;
                }
            }
        }

        public static PageResult<Flight> GetSearchFlightRequestPage(SearchFlightRequest request)
        {
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    var page = new PageResult<Flight>();

                    foreach (var flight in ctx.Flights.Include(x => x.From).Include(x => x.To))
                    {
                        if (flight.From.AirportName == request.From &&
                            flight.To.AirportName == request.To &&
                            flight.DepartureTime.Substring(0, 10) == request.DepartureDate)
                        {
                            page.TotalItems++;
                            page.Items.Add(flight);
                        }
                    }

                    return page;
                }
            }
        }

        public static bool IsFlightsPropNullOrEmpty(FlightRequest request)
        {
            lock (Locker.Lock)
            {
                return request.To == null ||
                       string.IsNullOrEmpty(request.To?.AirportName) ||
                       string.IsNullOrEmpty(request.To?.City) ||
                       string.IsNullOrEmpty(request.To?.Country) ||
                       request.From == null ||
                       string.IsNullOrEmpty(request.From?.AirportName) ||
                       string.IsNullOrEmpty(request.From?.City) ||
                       string.IsNullOrEmpty(request.From?.Country) ||
                       string.IsNullOrEmpty(request.Carrier) ||
                       string.IsNullOrEmpty(request.ArrivalTime) ||
                       string.IsNullOrEmpty(request.DepartureTime);
            }
        }

        public static bool IsArrivalTimeLessOrEqualToDepartureTime(FlightRequest request)
        {
            lock (Locker.Lock)
            {
              return DateTime.Compare(
                  DateTime.Parse(request.ArrivalTime), 
                  DateTime.Parse(request.DepartureTime)) <= 0;
            }
        }

        public static bool IsFromAndToAirportsAreSame(FlightRequest request)
        {
            lock (Locker.Lock)
            {
                return request.To.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim();
            }
        }

        public static bool IsFlightAlreadyInList(FlightRequest request)
        {
            lock (Locker.Lock)
            {
                using (var ctx = new FlightPlannerDbContext())
                { 
                    return ctx.Flights.Any(x => 
                        x.From.AirportName == request.From.AirportName && 
                        x.From.City == request.From.City && 
                        x.From.Country == request.From.Country && 
                        x.To.AirportName == request.To.AirportName && 
                        x.To.City == request.To.City && 
                        x.To.Country == request.To.Country && 
                        x.Carrier == request.Carrier && 
                        x.ArrivalTime == request.ArrivalTime && 
                        x.DepartureTime == request.DepartureTime
                     );
                }

            }
        }

        public static bool IsSearchFlightRequestInvalid(SearchFlightRequest request)
        {
            lock (Locker.Lock)
            {
                return request == null ||
                       request.To == request.From ||
                       string.IsNullOrEmpty(request.To) ||
                       string.IsNullOrEmpty(request.From) ||
                       string.IsNullOrEmpty(request.DepartureDate);
            }
        }
    }
}