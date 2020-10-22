using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Service.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;

            _mapper = mapper;

            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsers()
        {
            var users = await _userManager.Users.ToArrayAsync();

            var usersReadDto = _mapper.Map<IEnumerable<UserReadDto>>(users);

            foreach (var user in usersReadDto)
            {
                var role = (await _userManager.GetRolesAsync(_mapper.Map<User>(user))).FirstOrDefault();

                user.Role = role;
            }

            return usersReadDto;
        }

        public async Task<UserReadDto> GetUser(string email)
        {
            var users = await _userManager.FindByEmailAsync(email);

            if (users == null)
                throw new Exception("There is no user with such email");

            var userReadDto = _mapper.Map<UserReadDto>(users);

            userReadDto.Role = (await _userManager.GetRolesAsync(users)).FirstOrDefault();

            return userReadDto;
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
            var roleExists = await _roleManager.RoleExistsAsync(userCreateDto.Role);

            if (!roleExists)
                throw new Exception($"Role {userCreateDto.Role} doesn't exist.");

            var user = _mapper.Map<User>(userCreateDto);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, userCreateDto.Role);

            if (!addToRoleResult.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));
        }

        public async Task UpdateUser(UserReadDto userReadDto)
        {
            var user = await _userManager.FindByIdAsync(userReadDto.Id);

            if (user == null)
                throw new Exception("There is no such user");

            user.Name = userReadDto.Name;

            user.Surname = userReadDto.Surname;

            user.DateOfBirth = userReadDto.DateOfBirth;

            user.PhoneNumber = userReadDto.PhoneNumber;

            user.PassportId = userReadDto.PassportId;

            user.PassportSerialNumber = userReadDto.PassportSerialNumber;

            user.Email = userReadDto.Email;

            user.UserName = user.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception(string.Join("/r/n", result.Errors.Select(err => err.Description)));

            await UpdateUserRole(user, userReadDto.Role);

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

        public async Task UpdateUserRole(User user, string role)
        {
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (userRole == null)
            {
                await _userManager.AddToRoleAsync(user, role);

                return;
            }

            if (userRole != role)
            {
                var removeFromRole = await _userManager.RemoveFromRoleAsync(user, userRole);

                if (!removeFromRole.Succeeded)
                    throw new Exception(string.Join("/r/n", removeFromRole.Errors.Select(err => err.Description)));

                var addToRole = await _userManager.AddToRoleAsync(user, role);

                if (!addToRole.Succeeded)
                    throw new Exception(string.Join("/r/n", addToRole.Errors.Select(err => err.Description)));
            }
        }
    }
}
