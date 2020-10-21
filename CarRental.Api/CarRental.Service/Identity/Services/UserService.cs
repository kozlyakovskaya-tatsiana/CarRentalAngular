using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO;
using CarRental.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Service.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;

            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var user in usersDto)
            {
                var role = (await _userManager.GetRolesAsync(_mapper.Map<User>(user))).FirstOrDefault();

                user.Role = role;
            }

            return usersDto;
        }

        public async Task<UserDto> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("There is no user with such email");

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return userDto;
        }

        public async Task<bool> IsUserExists(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task CreateUser(UserCreatingModel model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, model.Role);

            if (!addToRoleResult.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));
        }

        public async Task UpdateUser(EditModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                throw new Exception("There is no such user");

            user.Name = model.Name;

            user.Surname = model.Surname;

            user.DateOfBirth = model.DateOfBirth;

            user.PhoneNumber = model.PhoneNumber;

            user.PassportId = model.PassportId;

            user.PassportSerialNumber = model.PassportSerialNumber;

            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Updating is failed.");
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception("There is no such user");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Deleting is failed.");
        }
    }
}
