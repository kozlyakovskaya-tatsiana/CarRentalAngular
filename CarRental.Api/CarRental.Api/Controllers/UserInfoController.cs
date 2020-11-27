using System.Threading.Tasks;
using CarRental.Api.Security;
using CarRental.Service.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForUsersAdmins)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public UserInfoController(IUserManagementService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>User</returns>
        /// <response code="200">Returns user.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">User with such id not found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }

        /// <summary>
        /// Get user's info.
        /// </summary>
        /// <returns>User's info.</returns>
        /// <response code="200">Returns user's info.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity.Name;

            var userInfo = await _userService.GetUserByEmail(userName);

            return Ok(userInfo);
        }
    }
}
