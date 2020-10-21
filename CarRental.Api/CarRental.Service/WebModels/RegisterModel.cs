using System;

namespace CarRental.Service.WebModels
{
    public class RegisterModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }

    }
}
