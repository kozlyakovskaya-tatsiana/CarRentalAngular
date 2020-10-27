using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.Service.Identity.Options;
using CarRental.Service.ServiceModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Service.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public TokenService(IOptions<JwtOptions> options, IRepository<RefreshToken> refreshTokenRepository)
        {
            _jwtOptions = options.Value;

            _refreshTokenRepository = refreshTokenRepository;

        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
                signingCredentials: new SigningCredentials(_jwtOptions.SymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.RefreshIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenLifeTime),
                signingCredentials: new SigningCredentials(_jwtOptions.SymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtOptions.SymmetricSecurityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _jwtOptions.RefreshIssuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }

        public async Task SaveTokenToDatabaseAsync(string token)
        {
            var refreshToken = new RefreshToken { RefreshTokenValue = token };

            await _refreshTokenRepository.CreateAsync(refreshToken);

            await _refreshTokenRepository.SaveChangesAsync();
        }

        public async Task<bool> IsTokenInDatabaseAsync(string token)
        {
            return (await _refreshTokenRepository.GetAsync()).SingleOrDefault(refToken => refToken.RefreshTokenValue == token) != null;
        }

        public async Task DeleteTokenFromDataBaseAsync(string token)
        {
            var refreshToken = (await _refreshTokenRepository.GetAsync()).SingleOrDefault(refToken => refToken.RefreshTokenValue == token);

            await _refreshTokenRepository.RemoveAsync(refreshToken.Id);

            await _refreshTokenRepository.SaveChangesAsync();
        }

        public async Task<TokenPair> GenerateTokenPairAsync(IEnumerable<Claim> claims)
        {
            var accessToken = GenerateToken(claims);

            var refreshToken = GenerateRefreshToken(claims);

            await SaveTokenToDatabaseAsync(refreshToken);

            return new TokenPair
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken
            };
        }

        public async Task<TokenPair> RefreshTokenAsync(string refreshToken)
        {
            if (!( await IsTokenInDatabaseAsync(refreshToken)))
            {
                throw new Exception("This token is invalid.");
            }

            await DeleteTokenFromDataBaseAsync(refreshToken);

            var principal = ValidateToken(refreshToken);

            var tokens = await  GenerateTokenPairAsync(principal.Claims); 

            return tokens;
        }
    }
}
