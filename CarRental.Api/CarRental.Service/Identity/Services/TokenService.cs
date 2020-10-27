using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public void SaveTokenToDatabase(string token)
        {
            var refreshToken = new RefreshToken { RefreshTokenValue = token };

            _refreshTokenRepository.Create(refreshToken);

            _refreshTokenRepository.SaveChanges();
        }

        public bool IsTokenInDatabase(string token)
        {
            return _refreshTokenRepository.Get().SingleOrDefault(refToken => refToken.RefreshTokenValue == token) != null;
        }

        public void DeleteTokenFromDataBase(string token)
        {
            var refreshToken = _refreshTokenRepository.Get().SingleOrDefault(refToken => refToken.RefreshTokenValue == token);

            _refreshTokenRepository.Remove(refreshToken.Id);

            _refreshTokenRepository.SaveChanges();
        }

        public TokenPair GenerateTokenPair(IEnumerable<Claim> claims)
        {
            var accessToken = GenerateToken(claims);

            var refreshToken = GenerateRefreshToken(claims);

            SaveTokenToDatabase(refreshToken);

            return new TokenPair
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken
            };
        }

        public TokenPair RefreshToken(string refreshToken)
        {
            if (!IsTokenInDatabase(refreshToken))
            {
                throw new Exception("This token is invalid.");
            }

            DeleteTokenFromDataBase(refreshToken);

            var principal = ValidateToken(refreshToken);

            var tokens = GenerateTokenPair(principal.Claims);

            return tokens;
        }
    }
}
