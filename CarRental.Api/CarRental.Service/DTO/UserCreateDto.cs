using System;

namespace CarRental.Service.DTO
{
    public class UserCreateDto : UserDto
    {
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
