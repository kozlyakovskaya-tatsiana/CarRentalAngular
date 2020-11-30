using System;

namespace CarRental.Service.DTO.RentalPointDtos
{
    public class RentalPointLocation
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Name { get; set; }
    }
}
