using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using CarRental.Service.WebModels.User;
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

        /// <summary>
        /// Update user's info.
        /// </summary>
        /// <param name="userService">Service used to update user</param>
        /// <param name="editUserBaseRequest">Data for updating user.</param>
        /// <returns>Result of updating.</returns>
        /// <response code="200">Updating is successful</response>
        /// <response code="400">EditUserBaseRequest validation is failed</response>
        /// <response code="401">Access denied. Authorization failed</response>
        /// <response code="404">User for updating is not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserBaseInfo(
            [FromServices] IUserManagementService userService,
            [FromBody] EditUserBaseRequest editUserBaseRequest)
        {
            var userToUpdate = _mapper.Map<UserDtoBase>(editUserBaseRequest);

            await userService.UpdateUserBaseInfo(userToUpdate);

            return Ok();
        }
    }
}
