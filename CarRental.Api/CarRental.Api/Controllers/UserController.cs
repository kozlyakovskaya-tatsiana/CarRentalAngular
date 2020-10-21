using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO;
using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
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

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _userService.GetUser(email);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreatingModel userCreatingModel)
        {
            var userToCreate = _mapper.Map<UserCreateDto>(userCreatingModel);

            await _userService.CreateUser(userToCreate);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]EditModel editModel)
        {
            var userToUpdate = _mapper.Map<UserEditDto>(editModel);

            await _userService.UpdateUser(userToUpdate);

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
