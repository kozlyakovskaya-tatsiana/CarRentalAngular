using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using CarRental.Identity.Models;

namespace CarRental.Identity
{ 
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);

        public string GenerateRefreshToken(IEnumerable<Claim> claims);

        ClaimsPrincipal ValidateToken(string token);
    }
}
