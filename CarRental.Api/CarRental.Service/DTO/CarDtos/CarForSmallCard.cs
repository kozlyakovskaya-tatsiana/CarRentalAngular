using System;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarForSmallCard
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public decimal CostPerDay { get; set; }

        public string Transmission { get; set; }

        public string Status { get; set; }

        public string PassengersAmount { get; set; }

        public string DoorsAmount { get; set; }

        public string ImageName { get; set; }
    }
}
