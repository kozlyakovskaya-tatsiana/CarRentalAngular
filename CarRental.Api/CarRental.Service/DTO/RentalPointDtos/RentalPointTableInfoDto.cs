using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.DTO.RentalPointDtos
{
    public class RentalPointTableInfoDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int CarsAmount { get; set; }
    }
}
