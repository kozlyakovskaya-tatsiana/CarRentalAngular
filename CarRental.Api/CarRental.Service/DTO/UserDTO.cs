﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO
{
    public class UserDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PassportSerialNumber { get; set; }

        public string PassportId { get; set; }

    }
}
