using CarRental.Identity;
using CarRental.Identity.Entities;
using CarRental.Identity.Models;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<User> _userManager;

        public AccountController(ILogger<AccountController> logger, ITokenService tokenService, IAuthorizeService authorizeService, UserManager<User> userManager)
        {
            _logger = logger;

            _tokenService = tokenService;

            _authorizeService = authorizeService;

            _userManager = userManager;

        }


        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            return Ok();
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
