using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CarRental.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PassportSerialNumber { get; set; }

        public string PassportId { get; set; }

        public List<BookingInfo> Bookings { get; set; }
    }
}
