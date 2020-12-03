using System;
using CarRental.DAL.Enums;

namespace CarRental.DAL.Entities
{
    public class BookingInfo : BaseEntity
    {
        public string PersonName { get; set; }

        public string PersonSurname { get; set; }

        public DateTime PersonDateOfBirth { get; set; }

        public string PersonPhoneNumber { get; set; }

        public string PersonPassportId { get; set; }

        public string PersonPassportSerialNumber { get; set; }

        public DateTime StartDateOfRenting { get; set; }

        public DateTime EndDateOfRenting { get; set; }

        public decimal Sum { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public Guid? CarId { get; set; }

        public Car Car { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
