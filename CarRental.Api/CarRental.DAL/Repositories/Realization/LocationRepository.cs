using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Realization
{
    public class LocationRepository : EfGenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationContext context) : base(context) {}

        public async Task<Location> GetLocationByAddressAsync(string address)
        {
            return (await GetAsync()).FirstOrDefault(loc => loc.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
    }
}
