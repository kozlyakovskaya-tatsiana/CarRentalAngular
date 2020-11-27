using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class RentalPoint: BaseEntity
    {
        public string Name { get; set; }

        public Location Location { get; set; }

        public Guid? LocationId { get; set; }

        public List<Car> Cars { get; set; }
    }
}
