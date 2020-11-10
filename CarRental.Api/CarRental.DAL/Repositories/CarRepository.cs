using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : EfGenericRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
            _includesFunc = query => query.Include(car => car.Documents);
        }

        public async Task<IEnumerable<Car>> GetCarsWithDocuments()
        {
            return await GetAllAsync(includes: _includesFunc);
        }
    }
}
