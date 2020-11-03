using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.UserDtos;

namespace CarRental.Service.Identity
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserReadDto>> GetUsers();

        Task<UserReadDto> GetUserByEmail(string email);

        Task<UserReadDto> GetUserById(Guid id);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserCreateDto userCreateDto);

        Task UpdateUser(UserUpdateDto userUpdateDto);

        Task DeleteUser(Guid id);

        Task UpdateUserBaseInfo(UserDtoBase userDtoBase);
    }
}
