using CarRental.DAL;
using CarRental.Identity.Models;
using CarRental.Identity.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarRental.Identity.Services
{
    public class TokenService
    {
        public readonly JwtOptions JwtOptions;

        private readonly DataStorage _dataStorage;

        public TokenService(IOptions<JwtOptions> options)
        {
            JwtOptions = options.Value;

            _dataStorage = DataStorage.GetDataStorage();
        }

        public string GenerateToken(IEnumerable<Claim> claims, int lifeTime)
        {
            var jwt = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(lifeTime),
                signingCredentials: new SigningCredentials(JwtOptions.SymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtOptions.SymmetricSecurityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal =
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }

        public ClaimsIdentity GetIdentity(LoginModel loginModel)
        {
            var user = _dataStorage.Users.FirstOrDefault((u => u.Email.Equals(loginModel.Email) && u.Password.Equals(loginModel.Password)));

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
}
