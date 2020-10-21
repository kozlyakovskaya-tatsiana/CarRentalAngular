using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using CarRental.Service.Identity;
using CarRental.Service.Models;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAuthorizeService _authorizeService;

        public AccountController(ILogger<AccountController> logger, IAuthorizeService authorizeService)
        {
            _logger = logger;

            _authorizeService = authorizeService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel loginModel)
        {
            var response = await _authorizeService.Login(loginModel);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            await _authorizeService.Register(registerModel);

            return Ok();
        }

    }
}
