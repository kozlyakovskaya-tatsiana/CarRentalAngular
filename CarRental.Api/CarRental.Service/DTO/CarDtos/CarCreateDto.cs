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

        public IFormFile MainImageFile { get; set; }
    }
}
