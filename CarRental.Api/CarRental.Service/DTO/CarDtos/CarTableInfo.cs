using System;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarTableInfo
    {
        public Guid Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public int ReleaseYear { get; set; }

        public string Status { get; set; }

        public decimal CostPerDay { get; set; }

        public string RentalPointName { get; set; }
    }
}
