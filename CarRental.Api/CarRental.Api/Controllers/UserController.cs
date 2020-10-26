using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForUserOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IMapper _mapper;

        public UserController(ILogger<AccountController> logger, IAuthorizeService authorizeService, IMapper mapper)
        {
            _logger = logger;

            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserBaseInfo(
            [FromServices] IUserManagementService userService,
            [FromBody] EditUserBaseRequest editUserBaseRequest
            )
        {
            var userToUpdate = _mapper.Map<UserDtoBase>(editUserBaseRequest);

            await userService.UpdateUserBaseInfo(userToUpdate);

            return Ok();
        }
    }
}
