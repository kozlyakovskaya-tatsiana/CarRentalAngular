using System;

namespace CarRental.DAL.Entities
{
    public class Image : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] ImageDataUrl { get; set; }

        public Car Car { get; set; }
    }
}
