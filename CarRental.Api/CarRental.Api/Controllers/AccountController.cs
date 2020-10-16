using CarRental.Identity;
using CarRental.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.Service;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                await _authorizeService.Register(registerModel);

                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

       [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel loginModel)
        {
            var response = await _authorizeService.Login(loginModel);

            return Ok(response);
        }

    }
}
