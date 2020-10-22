using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;

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
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginModel)
        {
            var response = await _authorizeService.Login(loginModel);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerModel)
        {
            await _authorizeService.Register(registerModel);

            return Ok();
        }

    }
}
