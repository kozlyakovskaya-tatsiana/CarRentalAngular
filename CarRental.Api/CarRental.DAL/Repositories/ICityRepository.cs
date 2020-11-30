using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetCityByNameAsync(string city);
    }
}
