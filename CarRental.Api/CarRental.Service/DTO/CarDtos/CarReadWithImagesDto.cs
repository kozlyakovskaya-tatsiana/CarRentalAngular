using System;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarReadWithImagesDto
    {
        public Guid Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string Carcase { get; set; }

        public int ReleaseYear { get; set; }

        public string Transmission { get; set; }

        public double EnginePower { get; set; }

        public double FuelConsumption { get; set; }

        public double TankVolume { get; set; }

        public string FuelType { get; set; }

        public string Status { get; set; }

        public double TrunkVolume { get; set; }

        public int PassengersAmount { get; set; }

        public int DoorsAmount { get; set; }

        public decimal CostPerDay { get; set; }

        public string RentalPointName { get; set; }

        public string[] ImageNames { get; set; }
    }
}
