using System;

namespace CarRental.Service.DTO.RentalPointDtos
{
    public class RentalPointTableInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int CarsAmount { get; set; }
    }
}
