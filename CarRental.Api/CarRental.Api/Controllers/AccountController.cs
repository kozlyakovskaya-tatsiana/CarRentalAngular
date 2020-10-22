using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO;
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

        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, IAuthorizeService authorizeService, IMapper mapper)
        {
            _logger = logger;

            _authorizeService = authorizeService;

            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
        {
            var response = await _authorizeService.Login(loginRequest);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _authorizeService.Register(registerRequest);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromServices]IUserManagementService userManagementService, EditUserBaseRequest editUserBaseRequest)
        {
            var user = _mapper.Map<UserDtoBase>(editUserBaseRequest);

            await userManagementService.UpdateUserBaseInfo(user);

            return Ok();
        }
    }
}
