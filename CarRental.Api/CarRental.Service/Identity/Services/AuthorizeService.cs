using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Service.Identity.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly UserManager<User> _userManager;

        private readonly ITokenService _tokenService;

        public AuthorizeService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;

            _tokenService = tokenService;
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

        public async Task Register(RegisterModel registerModel)
        {
            var user = new User {Email = registerModel.Email, UserName = registerModel.Email};

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                var addToToleResult = await _userManager.AddToRoleAsync(user, "user");
            }
            else
                throw new Exception(string.Join("/r/n",result.Errors.Select(err => err.Description)));
        }

        public async Task<bool> IsUserCanLogin(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Email);

            if (user == null)
                return false;

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            return checkPassword;
        }

        public async Task<LoginResponse> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Email);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var identity = GetIdentity(user.UserName, role);

            var accessToken = _tokenService.GenerateToken(identity.Claims);

            var refreshToken = _tokenService.GenerateRefreshToken(identity.Claims);

            _tokenService.SaveTokenToDatabase(refreshToken);

            return new LoginResponse
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                UserEmail = user.Email,

                UserRole = role
            };
        }

    }
}
