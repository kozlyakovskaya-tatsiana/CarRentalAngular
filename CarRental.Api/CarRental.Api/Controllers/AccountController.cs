using System.Threading.Tasks;
using CarRental.Service.Identity;
using CarRental.Service.WebModels.Authorize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        /// Allow user to log in.
        /// </summary>
        /// <param name="loginRequest">Data used to login.</param>
        /// <returns>If login is successful returns access and refresh tokens, user's role, user's email and user's id.</returns>
        /// <response code="200">Returns access and refresh tokens, user's role, user's email and user's id.</response>
        /// <response code="400">LoginRequest model fails validation.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
        {
            var response = await _authorizeService.Login(loginRequest);

            return Ok(response);
        }

        /// <summary>
        /// Register new users.
        /// </summary>
        /// <param name="registerRequest">Data used to register.</param>
        /// <returns>Success register.</returns>
        /// <response code="200">Registration is successful.</response>
        /// <response code="400">Validation failed.</response>
        /// <response code="500">Registration is failed.</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _authorizeService.Register(registerRequest);

            return Ok();
        }
    }
}
