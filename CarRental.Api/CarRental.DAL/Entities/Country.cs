using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class Country: BaseEntity
    {
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
