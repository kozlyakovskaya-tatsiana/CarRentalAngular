using CarRental.Api.Services;
using CarRental.BLL.Services;
using CarRental.Identity.Models;
using CarRental.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly TokenService _tokenService;

        private readonly AccountService _accountService;

        public AccountController(TokenService tokenService, ILogger<AccountController> logger, AccountService accountService)
        {
            _tokenService = tokenService;

            _logger = logger;

            _accountService = accountService;
        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody]LoginModel loginModel)
        {
            var identity = _tokenService.GetIdentity(loginModel);

            if (identity == null)
            {
                return BadRequest("No such user in the system.");
            }

            var accessToken = _tokenService.GenerateToken(identity.Claims, _tokenService.JwtOptions.LifeTime);

            var refreshToken = _tokenService.GenerateToken(identity.Claims, _tokenService.JwtOptions.RefreshTokenLifeTime);

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
