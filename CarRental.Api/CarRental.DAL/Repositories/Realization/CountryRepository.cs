using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Realization
{
    public class CountryRepository : EfGenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationContext context) : base(context) {}

        public async Task<Country> GetCountryByNameAsync(string country)
        {
            return (await GetAsync()).FirstOrDefault(c => c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
        }
    }
}
