using System;

namespace CarRental.DAL.Entities
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }

        public Car Car { get; set; }

        public Guid? CarId { get; set; }
    }
}
