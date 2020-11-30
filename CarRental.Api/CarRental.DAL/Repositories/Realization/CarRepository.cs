using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Realization
{
    public class CarRepositoryBase : RepositoryBase<Car>, ICarRepository
    {
        public CarRepositoryBase(ApplicationContext context) : base(context)
        {
            IncludesFunc = query => query.Include(car => car.Documents);
        }

        public async Task<IEnumerable<Car>> GetCarsWithDocuments()
        {
            return await GetAllAsync(includes: IncludesFunc);
        }
    }
}
