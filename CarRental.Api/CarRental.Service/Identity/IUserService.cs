using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.DTO;
using CarRental.Service.Models;
using CarRental.Service.WebModels;

namespace CarRental.Service.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<UserShowDto>> GetUsers();

        Task<UserShowDto> GetUser(string email);

        Task<bool> IsUserExists(string email, string password);

        Task CreateUser(UserCreateDto userDto);

        Task UpdateUser(UserShowDto userShowDto);

        Task DeleteUser(string id);
    }
}
