using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ITokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> FindTokenAsync(string token);
    }
}
