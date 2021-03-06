using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> GetLocationByAddressAsync(string address);
    }
}
