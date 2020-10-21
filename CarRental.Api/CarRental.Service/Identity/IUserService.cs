using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.DTO;
using CarRental.Service.Models;

namespace CarRental.Service.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<UserDto> GetUser(string email);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserCreatingModel model);

        Task UpdateUser(EditModel model);

        Task DeleteUser(string id);
    }
}
