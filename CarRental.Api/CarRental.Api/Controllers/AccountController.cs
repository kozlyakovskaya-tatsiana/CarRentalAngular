using CarRental.Identity;
using CarRental.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly ITokenService _tokenService;

        private readonly IAuthorizeService _authorizeService;

        public AccountController(ILogger<AccountController> logger, ITokenService tokenService, IAuthorizeService authorizeService)
        {
            _logger = logger;

            _tokenService = tokenService;

            _authorizeService = authorizeService;

        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody]LoginModel loginModel)
        {
            var identity = _authorizeService.GetIdentity(loginModel);

            if (identity == null)
            {
                return BadRequest("No such user in the system.");
            }

            var accessToken = _tokenService.GenerateToken(identity.Claims);

            var refreshToken = _tokenService.GenerateRefreshToken(identity.Claims);

            var response = new
            {
                access_token = accessToken,

                refresh_token = refreshToken,

                userEmail = identity.Name
            };

            return Ok(response);
        }

    }
}
