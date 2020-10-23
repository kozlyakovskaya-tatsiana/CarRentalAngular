using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO;

namespace CarRental.Service.Identity
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserReadDto>> GetUsers();

        Task<UserReadDto> GetUserByEmail(string email);

        Task<UserReadDto> GetUserById(string id);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserCreateDto userCreateDto);

        Task UpdateUser(UserUpdateDto userUpdateDto);

        Task DeleteUser(string id);

        Task UpdateUserBaseInfo(UserDtoBase userDtoBase);
    }
}
