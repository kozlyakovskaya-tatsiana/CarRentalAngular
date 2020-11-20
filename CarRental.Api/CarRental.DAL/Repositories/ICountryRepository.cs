using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetCountryByNameAsync(string country);
    }
}
