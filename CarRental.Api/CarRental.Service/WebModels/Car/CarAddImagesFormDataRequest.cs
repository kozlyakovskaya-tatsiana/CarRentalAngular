using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.WebModels.Car
{
    public class CarAddImagesFormDataRequest
    {
        public Guid CarId { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
