using System;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarReadDto
    {
        public Guid Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string Carcase { get; set; }

        public int ReleaseYear { get; set; }

        public string Transmission { get; set; }

        public string FuelType { get; set; }
    }
}
