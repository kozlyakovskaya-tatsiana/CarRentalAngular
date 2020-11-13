using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarForSmallCardDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public decimal CostPerDay { get; set; }

        public string Transmission { get; set; }

        public string Status { get; set; }

        public string PassengersAmount { get; set; }

        public string DoorsAmount { get; set; }

        public string ImageName { get; set; }
    }
}
