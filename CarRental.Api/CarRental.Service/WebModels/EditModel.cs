﻿using System;

namespace CarRental.Service.WebModels
{
    public class EditModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PassportSerialNumber { get; set; }

        public string PassportId { get; set; }

        public string Role { get; set; }

    }
}
