using System;
using CarRental.DAL.Enums;

namespace CarRental.Service.DTO.BookingDtos
{
    public class BookingInfoForRead
    {
        public string BookingId { get; set; }

        public string PersonName { get; set; }

        public string PersonSurname { get; set; }

        public DateTime PersonDateOfBirth { get; set; }

        public string PersonPhoneNumber { get; set; }

        public string PersonPassportId { get; set; }

        public string PersonPassportSerialNumber { get; set; }

        public DateTime StartDateOfRenting { get; set; }

        public DateTime EndDateOfRenting { get; set; }

        public decimal Sum { get; set; }

        public string BookingStatusName { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public Guid? CarId { get; set; }

        public string CarName { get; set; }

        public string CarImageName { get; set; }

        public string RentalPointName { get; set; }

        public string RentalPointAddress { get; set; }

        public Guid RentalPointId { get; set; }
    }
}
