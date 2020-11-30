using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;

namespace CarRental.Service.DTO.BookingDtos
{
    public class BookingRequestInfo
    {
        public string PersonName { get; set; }

        public string PersonSurname { get; set; }

        public DateTime PersonDateOfBirth { get; set; }

        public string PersonPhoneNumber { get; set; }

        public string PersonPassportId { get; set; }

        public string PersonPassportSerialNumber { get; set; }

        public DateTime StartDateOfRenting { get; set; }

        public DateTime EndDateOfRenting { get; set; }

        public decimal Sum;

        public BookingStatus BookingStatus { get; set; }

        public string CarName { get; set; }

        public string CarImagePath { get; set; }
    }
}
