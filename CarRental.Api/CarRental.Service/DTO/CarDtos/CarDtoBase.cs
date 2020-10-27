using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Enums;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarDtoBase
    {
        public string Mark { get; set; }

        public string Model { get; set; }

        public Carcase Carcase { get; set; }

        public int ReleaseYear { get; set; }

        public Transmission Transmission { get; set; }

        public double EnginePower { get; set; }

        public double FuelConsumption { get; set; }

        public double TankVolume { get; set; }

        public FuelType FuelType { get; set; }

        public double TrunkVolume { get; set; }

    }
}
