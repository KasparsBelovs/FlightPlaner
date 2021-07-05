using System.Collections.Generic;
using System.Linq;
using FlightPlannerVS.Core.Models;

namespace FlightPlannerVS.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        IQueryable<T> Query();
        IEnumerable<T> Get();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
