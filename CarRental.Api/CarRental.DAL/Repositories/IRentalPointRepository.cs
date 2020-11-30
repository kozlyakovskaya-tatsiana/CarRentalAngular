using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface IRentalPointRepository : IRepository<RentalPoint>
    {
        Task<IEnumerable<RentalPoint>> GetRentalPointsWithLocations(Expression<Func<RentalPoint, bool>> predicate = null);

        Task<RentalPoint> GetRentalPointByNameAsync(string name);
    }
}
