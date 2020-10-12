using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiniWebApi.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebApi.Services
{
    public class TokenService
    {
        public readonly JwtOptions JwtOptions;

        public TokenService(IOptions<JwtOptions> options)
        {
            JwtOptions = options.Value;
        }

        public string GenerateToken(IEnumerable<Claim> claims, int lifeTime)
        {
            var jwt = new JwtSecurityToken(
                    issuer: JwtOptions.Issuer,
                    audience: JwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(lifeTime),
                    signingCredentials: new SigningCredentials(JwtOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

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
         
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }
    }
}
