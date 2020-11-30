using System;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.WebModels.Car
{
    public class CarAddImagesRequest
    {
        public Guid CarId { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
