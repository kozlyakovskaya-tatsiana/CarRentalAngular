using System;
using System.Collections.Generic;
using System.Security.Claims;
using CarRental.Api.Services;
using CarRental.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;

        private readonly ILogger<TokenController> _logger;

        public TokenController(TokenService tokenService, ILogger<TokenController> logger)
        {
            _tokenService = tokenService;

            _logger = logger;
        }

        /// <summary>
        /// Method provides receiving token and refresh token.
        /// </summary>
        /// <returns>JWT tokens (string format)</returns>
        /// <response code="200">Return JWT tokens.</response>
        [HttpGet("token")]
        [ProducesResponseType(200)]
        public IActionResult GetToken()
        {
            _logger.LogInformation("User send request to get token");

            var claims = new List<Claim>();

            claims.Add(new Claim("role", "user"));

            claims.Add(new Claim("someClaim", "someContent"));

            var accessToken = _tokenService.GenerateToken(claims, _tokenService.JwtOptions.LifeTime);

            var refreshToken = _tokenService.GenerateToken(claims, _tokenService.JwtOptions.RefreshTokenLifeTime);

            return Ok(new
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Method provides refresh tokens.
        /// </summary>
        /// <param name="refreshToken">Refresh token.</param>
        /// <returns>New pair of jwt tokens.</returns>
        /// <response code="200">Return JWT tokens.</response>
        /// <response code="400">Incorrect client data.</response>
        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult RefreshToken(string refreshToken)
        {
            try
            {
                _logger.LogInformation("User send request to get refresh token");

                if (String.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest("Invalid client request");
                }

                var principal = _tokenService.ValidateToken(refreshToken);

                var claims = principal.Claims;

                var accessToken = _tokenService.GenerateToken(claims, _tokenService.JwtOptions.LifeTime);

                var newRefreshToken = _tokenService.GenerateToken(claims, _tokenService.JwtOptions.RefreshTokenLifeTime);

                return Ok(new
                {
                    AccessToken = accessToken,

                    RefreshToken = newRefreshToken
                });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
