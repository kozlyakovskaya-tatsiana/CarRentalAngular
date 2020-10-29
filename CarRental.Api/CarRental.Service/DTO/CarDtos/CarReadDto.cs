﻿using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Enums;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarReadDto
    {
        public int Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string Carcase { get; set; }

        public int ReleaseYear { get; set; }

        public string Transmission { get; set; }

        public double EnginePower { get; set; }

        public double FuelConsumption { get; set; }

        public double TankVolume { get; set; }

        public string FuelType { get; set; }

        public double TrunkVolume { get; set; }
    }
}