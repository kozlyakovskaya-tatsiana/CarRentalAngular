﻿namespace CarRental.Service.DTO.UserDtos
{
    public class UserCreateDto : UserDtoBase
    {
        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }
    }
}