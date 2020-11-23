using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class Location : BaseEntity
    {
        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public City City { get; set; }

        public Guid? CityId { get; set; }

        public RentalPoint RentalPoint { get; set; }
    }
}
