using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.UserDtos;

namespace CarRental.Service.Identity
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserForRead>> GetUsers();

        Task<UserForRead> GetUserByEmail(string email);

        Task<UserForRead> GetUserById(string id);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserForCreate userCreateDto);

        Task UpdateUser(UserForUpdate userUpdateDto);

        Task DeleteUser(string id);

        Task UpdateUserBaseInfo(UserBase userDtoBase);
    }
}
