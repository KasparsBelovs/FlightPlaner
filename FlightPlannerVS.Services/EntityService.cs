using System.Collections.Generic;
using System.Linq;
using FlightPlannerVS.Core.Models;
using FlightPlannerVS.Core.Services;
using FlightPlannerVS.Data;

namespace FlightPlannerVS.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        protected readonly IFlightPlannerDbContext _context;

        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Update(T entity)
        {
           Update<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }
    }
}
