using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO;
using CarRental.Service.Models;
using CarRental.Service.WebModels;
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

        public async Task<IEnumerable<UserShowDto>> GetUsers()
        {
            var users = await _userManager.Users.ToArrayAsync();

             var usersShowDto = _mapper.Map<IEnumerable<UserShowDto>>(users);

            foreach (var user in usersShowDto)
            {
                var role = (await _userManager.GetRolesAsync(_mapper.Map<User>(user))).FirstOrDefault();

                user.Role = role;
            }

            return usersShowDto;
        }

        public async Task<UserShowDto> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("There is no user with such email");

            var userShowDto = _mapper.Map<UserShowDto>(user);

            userShowDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return userShowDto;
        }

        public async Task<bool> IsUserExists(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task CreateUser(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userCreateDto.Role);

            if (!addToRoleResult.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));
        }

        public async Task UpdateUser(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByIdAsync(userEditDto.Id);

            if (user == null)
                throw new Exception("There is no such user");

            user.Name = userEditDto.Name;

            user.Surname = userEditDto.Surname;

            user.DateOfBirth = userEditDto.DateOfBirth;

            user.PhoneNumber = userEditDto.PhoneNumber;

            user.PassportId = userEditDto.PassportId;

            user.PassportSerialNumber = userEditDto.PassportSerialNumber;

            user.Email = userEditDto.Email;

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
