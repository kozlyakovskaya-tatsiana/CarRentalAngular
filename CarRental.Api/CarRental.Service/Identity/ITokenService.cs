using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Service.ServiceModels;

namespace CarRental.Service.Identity
{ 
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);

        public string GenerateRefreshToken(IEnumerable<Claim> claims);

        ClaimsPrincipal ValidateToken(string token);

        Task SaveTokenToDatabaseAsync(string token);

        Task<bool> IsTokenInDatabaseAsync(string token);

        Task DeleteTokenFromDataBaseAsync(string token);

        Task<TokenPair> GenerateTokenPairAsync(IEnumerable<Claim> claims);

        Task<TokenPair> RefreshTokenAsync(string refreshToken);

    }
}
