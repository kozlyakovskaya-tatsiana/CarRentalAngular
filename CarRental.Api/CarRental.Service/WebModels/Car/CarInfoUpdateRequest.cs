using System;
using CarRental.DAL.Enums;

namespace CarRental.Service.WebModels.Car
{
    public class CarInfoUpdateRequest
    {
        public Guid Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public CarcaseType Carcase { get; set; }

        public int ReleaseYear { get; set; }

        public TransmissionType Transmission { get; set; }

        public double EnginePower { get; set; }

        public double FuelConsumption { get; set; }

        public double TankVolume { get; set; }

        public FuelType FuelType { get; set; }

        public double TrunkVolume { get; set; }

        public int PassengersAmount { get; set; }

        public int DoorsAmount { get; set; }

        public CarStatus Status { get; set; }

        public decimal CostPerDay { get; set; }

        public string RentalPointName { get; set; }
    }
}
