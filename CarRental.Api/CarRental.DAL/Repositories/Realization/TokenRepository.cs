using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Realization
{
    public class TokenRepositoryBase : RepositoryBase<RefreshToken>, ITokenRepository
    {
        public async Task<RefreshToken> FindTokenAsync(string queryToken)
        {
            var token = await DbSet.FirstOrDefaultAsync(t => t.RefreshTokenValue.Equals(queryToken));

            return token;
        }

        public TokenRepositoryBase(ApplicationContext context) : base(context) { }
    }
}
