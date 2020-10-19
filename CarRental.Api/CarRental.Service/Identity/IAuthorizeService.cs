using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Service.Models;

namespace CarRental.Service.Identity
{
    public interface IAuthorizeService
    {
        ClaimsIdentity GetIdentity(string userName, string userRole);

        Task Register(RegisterModel registerModel);

        Task<bool> IsUserCanLogin(LoginModel loginModel);

        Task<LoginResponse> Login(LoginModel loginModel);
    }
}
