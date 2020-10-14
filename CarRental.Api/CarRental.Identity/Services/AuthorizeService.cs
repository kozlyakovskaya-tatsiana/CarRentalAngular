using CarRental.Identity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CarRental.DAL;

namespace CarRental.Identity.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly DataStorage _dataStorage;

        public AuthorizeService(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public ClaimsIdentity GetIdentity(LoginModel loginModel)
        {
            var user = _dataStorage.Users.FirstOrDefault((u => u.Email.Equals(loginModel.Email) && u.Password.Equals(loginModel.Password)));

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
            }

            return null;
        }
    }
}
