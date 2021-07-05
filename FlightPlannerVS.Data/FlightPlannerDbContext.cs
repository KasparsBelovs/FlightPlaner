using System.Data.Entity;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Data.Migrations;


namespace FlightPlannerVS.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDbContext>(
                new MigrateDatabaseToLatestVersion<
                    FlightPlannerDbContext, Configuration
                >());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
