using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    //[Authorize(Policy = "ForAdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserManagementService userService, IMapper mapper)
        {
            _userService = userService;

            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }

        [Authorize(Policy = "ForUsersAdmins")]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity.Name;

            var userInfo = await _userService.GetUserByEmail(userName);

            return Ok(userInfo);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreatingRequest userCreatingRequest)
        {
            var userToCreate = _mapper.Map<UserCreateDto>(userCreatingRequest);

            await _userService.CreateUser(userToCreate);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]EditUserRequest editUserRequest)
        {
            var userToUpdate = _mapper.Map<UserUpdateDto>(editUserRequest);

            await _userService.UpdateUser(userToUpdate);

            return Ok();
        }

        [HttpPut("updatebaseinfo")]
        public async Task<IActionResult> UpdateUserBaseInfo([FromBody] EditUserBaseRequest editUserBaseRequest)
        {
            var userToUpdate = _mapper.Map<UserDtoBase>(editUserBaseRequest);

            await _userService.UpdateUserBaseInfo(userToUpdate);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }
    }
}
