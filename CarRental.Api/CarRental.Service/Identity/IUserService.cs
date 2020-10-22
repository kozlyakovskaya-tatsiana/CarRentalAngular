using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO;

namespace CarRental.Service.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetUsers();

        Task<UserReadDto> GetUser(string email);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserCreateDto userCreateDto);

        Task UpdateUser(UserReadDto userReadDto);

        Task DeleteUser(string id);
    }
}
