using System.Threading.Tasks;
using AutoMapper;
using CarRental.Api.Security;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Identity;
using CarRental.Service.WebModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForUserOnly)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IMapper _mapper;

        private readonly IUserManagementService _userManagementService;

        public UserController(ILogger<AccountController> logger, IAuthorizeService authorizeService, IMapper mapper, IUserManagementService userManagementService)
        {
            _logger = logger;

            _mapper = mapper;

            _userManagementService = userManagementService;
        }

        /// <summary>
        /// Update user's info.
        /// </summary>
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
        public async Task<IActionResult> Update([FromBody] EditUserBaseRequest editUserBaseRequest)
        {
            var userToUpdate = _mapper.Map<UserBase>(editUserBaseRequest);

            await _userManagementService.UpdateUserBaseInfo(userToUpdate);

            return Ok();
        }
    }
}
