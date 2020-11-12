using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarAddImagesDto
    {
        public Guid CarId { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
