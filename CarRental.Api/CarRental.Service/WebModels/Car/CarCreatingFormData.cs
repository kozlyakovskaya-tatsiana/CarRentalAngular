using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.WebModels.Car
{
    public class CarCreatingFormData
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

        public IFormFile MainImgFile { get; set; }
    }
}
