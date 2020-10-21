using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO
{
    public class UserShowDto : UserDto
    {
        public string Id { get; set; }

        public string Role { get; set; }
    }
}
