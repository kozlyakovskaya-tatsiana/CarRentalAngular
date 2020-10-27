using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForUsersAdmins")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<RoleController> logger)
        {
            _roleManager = roleManager;

            _mapper = mapper;

            _logger = logger;
        }

        [HttpGet("roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(role => role.Name).ToArrayAsync();

            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var role = new IdentityRole(roleName);

            await _roleManager.CreateAsync(role);

            return Ok();
        }


    }
}
