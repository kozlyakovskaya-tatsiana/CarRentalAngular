using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Realization
{
    public class RentalPointRepositoryBase : RepositoryBase<RentalPoint>, IRentalPointRepository
    {
        public RentalPointRepositoryBase(ApplicationContext context) : base(context) { }

        public async Task<RentalPoint> GetRentalPointByNameAsync(string name)
        {
            return (await GetAsync(p => p.Name.Equals(name))).FirstOrDefault();
        }

        public async Task<IEnumerable<RentalPoint>> GetRentalPointsWithLocations(Expression<Func<RentalPoint, bool>> predicate = null)
        {
            return await (await GetAllAsync(predicate, point =>
                point.Include(p => p.Location.City.Country))).ToArrayAsync();
        }
    }
}
