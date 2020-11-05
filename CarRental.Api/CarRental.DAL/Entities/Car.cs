using System;
using System.Collections.Generic;
using CarRental.DAL.Enums;

namespace CarRental.DAL.Entities
{
    public class Car : IEntity
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
        
        public List<Document> Documents { get; set; }
    }
}
