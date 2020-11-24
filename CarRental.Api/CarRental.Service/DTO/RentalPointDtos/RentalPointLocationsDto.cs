using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO.RentalPointDtos
{
    public class RentalPointLocationsDto
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
