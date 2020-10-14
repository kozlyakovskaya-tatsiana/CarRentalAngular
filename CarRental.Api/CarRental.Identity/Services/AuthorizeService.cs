using CarRental.Identity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CarRental.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Identity.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly UserManager<User> _userManager;

        public AuthorizeService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public ClaimsIdentity GetIdentity(string userName, string userRole)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
                };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
