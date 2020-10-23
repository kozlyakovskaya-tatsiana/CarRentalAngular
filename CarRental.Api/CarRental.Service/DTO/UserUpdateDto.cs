using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO
{
    public class UserUpdateDto : UserDtoBase
    {
        public string Role { get; set; }
    }
}
