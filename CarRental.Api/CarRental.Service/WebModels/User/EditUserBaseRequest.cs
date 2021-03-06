using System;

namespace CarRental.Service.WebModels.User
{
    public class EditUserBaseRequest
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
