using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Realization
{
    public class LocationRepositoryBase : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepositoryBase(ApplicationContext context) : base(context) {}

        public async Task<Location> GetLocationByAddressAsync(string address)
        {
            return (await GetAsync()).FirstOrDefault(loc => loc.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
    }
}
