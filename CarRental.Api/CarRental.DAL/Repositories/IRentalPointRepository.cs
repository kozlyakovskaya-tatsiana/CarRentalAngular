using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface IRentalPointRepository : IRepository<RentalPoint>
    {
        Task<IEnumerable<RentalPoint>> GetRentalPointsWithLocations();

        Task<RentalPoint> GetRentalPointByNameAsync(string name);
    }
}
