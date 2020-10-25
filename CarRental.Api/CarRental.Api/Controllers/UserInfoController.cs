using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForUsersAdmins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        private readonly IMapper _mapper;

        public UserInfoController(IUserManagementService userService, IMapper mapper)
        {
            _userService = userService;

            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity.Name;

            var userInfo = await _userService.GetUserByEmail(userName);

            return Ok(userInfo);

        }

    }
}
