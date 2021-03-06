using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICarRepository: IRepository<Car>
    {
        Task<IEnumerable<Car>> GetCarsWithDocuments();
    }
}
