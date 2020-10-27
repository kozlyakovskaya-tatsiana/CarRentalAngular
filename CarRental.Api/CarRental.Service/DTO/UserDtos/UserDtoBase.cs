using System;

namespace CarRental.Service.DTO.UserDtos
{
    public class UserDtoBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportSerialNumber { get; set; }

        public string PassportId { get; set; }

    }
}
