using System;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarForAddImages
    {
        public Guid CarId { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
