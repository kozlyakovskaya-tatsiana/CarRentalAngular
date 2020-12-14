using System.Linq;
using System.Threading.Tasks;
using CarRental.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForUsersAdmins)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger)
        {
            _roleManager = roleManager;

            _logger = logger;
        }

        /// <summary>
        /// Get array of roles/
        /// </summary>
        /// <returns>Array of roles.</returns>
        /// <response code="200">Returns the array of roles.</response>
        [HttpGet("roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(role => role.Name).ToArrayAsync();

            return Ok(roles);
        }
    }
}
