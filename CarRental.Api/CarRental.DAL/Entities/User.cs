using System.ComponentModel.DataAnnotations;

namespace CarRental.DAL.Entities
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }
    }
}
