using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Identity.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PassportSerialNumber { get; set; }

        public string PassportId { get; set; }
    }
}
