using System.Threading.Tasks;
using CarRental.Service.Identity;
using CarRental.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel registerModel)
        {
            await _userService.CreateUser(registerModel);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]EditModel user)
        {
            await _userService.UpdateUser(user);

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
