using System;
using System.Collections.Generic;
using CarRental.DAL.Enums;

namespace CarRental.DAL.Entities
{
    public class Car : BaseEntity
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

        public int PassengersAmount { get; set; }

        public int DoorsAmount { get; set; }

        public Status Status { get; set; }

        public decimal CostPerDay { get; set; }

        public List<Document> Documents { get; set; }

        public Guid? RentalPointId { get; set; }

        public RentalPoint RentalPoint { get; set; }
    }
}
