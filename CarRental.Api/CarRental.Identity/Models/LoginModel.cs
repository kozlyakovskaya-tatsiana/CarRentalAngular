using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarRental.Identity.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Email must be in email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }
    }
}
