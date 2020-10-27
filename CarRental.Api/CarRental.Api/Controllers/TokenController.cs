using System;
using System.Collections.Generic;
using System.Security.Claims;
using CarRental.Service.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger)
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

            var claims = new List<Claim>
            {
                new Claim("role", "user"),

                new Claim("someClaim", "someContent")
            };

            var tokens = _tokenService.GenerateTokenPairAsync(claims);

            return Ok(tokens);
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
        public IActionResult RefreshToken([FromBody]string refreshToken)
        {
            _logger.LogInformation("User send request to get refresh token");

            if (String.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Invalid client request");
            }

            var tokens = _tokenService.RefreshTokenAsync(refreshToken);

            return Ok(tokens);
        }
    }
}
