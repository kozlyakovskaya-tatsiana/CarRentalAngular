using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Realization
{
    public class CityRepository : EfGenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationContext context) : base(context) {}

        public async Task<City> GetCityByNameAsync(string city)
        {
            return (await GetAsync()).FirstOrDefault(c => c.Name.Equals(city, StringComparison.OrdinalIgnoreCase));
        }
    }
}
