using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForAdminOnly")]
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreatingRequest userCreatingRequest)
        {
            var userToCreate = _mapper.Map<UserCreateDto>(userCreatingRequest);

            await _userService.CreateUser(userToCreate);

            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserRequest editUserRequest)
        {
            var userToUpdate = _mapper.Map<UserUpdateDto>(editUserRequest);

            await _userService.UpdateUser(userToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }
    }
}
