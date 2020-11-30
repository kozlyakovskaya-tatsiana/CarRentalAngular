using System.Threading.Tasks;
using AutoMapper;
using CarRental.Api.Security;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForAdminOnly)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        private readonly IMapper _mapper;

        public AdminController(IUserManagementService userService, IMapper mapper)
        {
            _userService = userService;

            _mapper = mapper;
        }

        /// <summary>
        /// Get array of users.
        /// </summary>
        /// <returns>An array of users</returns>
        /// <response code="200">Return array of users</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="500">Server error</response>
        [HttpGet("userslist")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUsersList()
        {
            var users = await _userService.GetUsers();

            return Ok(users);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="userCreatingRequest">Data used to create user</param>
        /// <returns>Result of creating user</returns>
        /// <response code="200">Successful creating user</response>
        /// <response code="400">UserCreatingModel failed validation</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">User's role doesn't exist</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreatingRequest userCreatingRequest)
        {
            var userToCreate = _mapper.Map<UserForCreate>(userCreatingRequest);

            await _userService.CreateUser(userToCreate);

            return Ok();
        }

        /// <summary>
        /// Update user's info.
        /// </summary>
        /// <param name="editUserRequest">Information for editing user</param>
        /// <returns>Result of updating</returns>
        /// <response code="200">Updating is successful.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">User for updating is not found.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserRequest editUserRequest)
        {
            var userToUpdate = _mapper.Map<UserForUpdate>(editUserRequest);

            await _userService.UpdateUser(userToUpdate);

            return Ok();
        }

        /// <summary>
        /// Deleting of user.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Deleting is successful.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">User for deleting is not found</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }
    }
}
