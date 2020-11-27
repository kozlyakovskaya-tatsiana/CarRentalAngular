using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class BookingInfo : BaseEntity
    {
        public string PersonName { get; set; }

        public string PersonSurname { get; set; }

        public DateTime PersonDateOfBirth { get; set; }

        public string PersonPhoneNumber { get; set; }

        public string PersonPassportId { get; set; }

        public string PersonSerialNumber { get; set; }

        public DateTime StartDateOfRenting { get; set; }

        public DateTime EndDateOfRenting { get; set; }

        public Guid? CarId { get; set; }

        public Car Car { get; set; }
    }
}
