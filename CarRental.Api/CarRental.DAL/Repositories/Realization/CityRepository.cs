using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Realization
{
    public class CityRepositoryBase : RepositoryBase<City>, ICityRepository
    {
        public CityRepositoryBase(ApplicationContext context) : base(context) {}

        public async Task<City> GetCityByNameAsync(string city)
        {
            return (await GetAsync()).FirstOrDefault(c => c.Name.Equals(city, StringComparison.OrdinalIgnoreCase));
        }
    }
}
