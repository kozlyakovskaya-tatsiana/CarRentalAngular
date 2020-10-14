using CarRental.Identity;
using CarRental.Identity.Entities;
using CarRental.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger, ITokenService tokenService, IAuthorizeService authorizeService,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;

            _tokenService = tokenService;

            _authorizeService = authorizeService;

            _userManager = userManager;

            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                var user = new User { Email = registerModel.Email, UserName = registerModel.Email };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                    return Ok();

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);

            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Email);

            if (user == null)
            {
                return BadRequest("There is no account with such email.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest("Password is incorrect.");
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "myeditrole";

            var identity = _authorizeService.GetIdentity(user.UserName, role);

            var accessToken = _tokenService.GenerateToken(identity.Claims);

            var refreshToken = _tokenService.GenerateRefreshToken(identity.Claims);

            var response = new
            {
                access_token = accessToken,

                refresh_token = refreshToken,

                userEmail = identity.Name,

                userRole = role
            };

            return Ok(response);
        }

    }
}
