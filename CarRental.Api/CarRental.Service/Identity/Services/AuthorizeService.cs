using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.WebModels.Authorize;
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

        public async Task<LoginResponse> Login(LoginRequest loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Email);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var identity = GetIdentity(user.UserName, role);

            var accessToken = _tokenService.GenerateToken(identity.Claims);

            var refreshToken = _tokenService.GenerateRefreshToken(identity.Claims);

            await _tokenService.SaveTokenToDatabaseAsync(refreshToken);

            return new LoginResponse
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                UserEmail = user.Email,

                UserRole = role,

                UserId = user.Id
            };
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var user = new User
            {
                Name = registerRequest.Name,

                Surname = registerRequest.Surname,

                DateOfBirth = registerRequest.DateOfBirth,

                PhoneNumber = registerRequest.PhoneNumber,

                Email = registerRequest.Email,
                
                UserName = registerRequest.Email
            };
            
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                var addToToleResult = await _userManager.AddToRoleAsync(user, "user");
            }
            else
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));
        }
    }
}
