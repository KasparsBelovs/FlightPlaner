using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Data;

namespace FlightPlannerVS.Services
{
    //can be used only with EntityService. this one can be deleted
    public class DbService : IDbService
    {
        private readonly IFlightPlannerDbContext _context;

        public DbService(IFlightPlannerDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Get<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public void Create<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
