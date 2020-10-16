using System.Collections.Generic;
using System.Security.Claims;

namespace CarRental.Service.Identity
{ 
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);

        public string GenerateRefreshToken(IEnumerable<Claim> claims);

        ClaimsPrincipal ValidateToken(string token);

        void SaveTokenToDatabase(string token);

        bool IsTokenInDatabase(string token);

        void DeleteTokenFromDataBase(string token);

    }
}
