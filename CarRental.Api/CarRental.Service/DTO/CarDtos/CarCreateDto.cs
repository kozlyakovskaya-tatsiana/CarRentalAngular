using CarRental.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarCreateDto
    {
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

        public Status Status { get; set; }

        public int PassengersAmount { get; set; }

        public int DoorsAmount { get; set; }

        public decimal CostPerDay { get; set; }

        public string RentalPointName { get; set; }

        public IFormFile[] Images { get; set; }

        public string PathToStoreImages { get; set; }
    }
}
