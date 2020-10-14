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
        

        public ClaimsIdentity GetIdentity(LoginModel loginModel)
        {
            /*var user = _userManager.FindByLoginAsync(loginModel.Email, loginModel.Password);

            
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }*/

            return null;
        }
    }
}
