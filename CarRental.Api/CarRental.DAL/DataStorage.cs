using CarRental.Identity.Entities;
using System.Collections.Generic;
using CarRental.DAL.Entities;


namespace CarRental.DAL
{
    public class DataStorage
    {
        public List<User> Users { get; set; }

        public List<string> Values { get; set; } 

        public DataStorage()
        {
            Values = new List<string>(new[] { "value1", "value2", "value3", "value4" });
        }
    }
}
