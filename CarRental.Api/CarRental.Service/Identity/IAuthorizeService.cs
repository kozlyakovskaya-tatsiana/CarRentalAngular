using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Service.WebModels;

namespace CarRental.Service.Identity
{
    public interface IAuthorizeService
    {
        ClaimsIdentity GetIdentity(string userName, string userRole);

        Task Register(RegisterRequest registerRequest);

        Task<LoginResponse> Login(LoginRequest loginModel);
    }
}
