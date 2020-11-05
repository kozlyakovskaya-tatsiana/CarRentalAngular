using System;

namespace CarRental.DAL.Entities
{
    public class ImageFile : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
