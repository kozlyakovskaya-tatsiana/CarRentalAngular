using System;
using System.Collections.Generic;
using CarRental.Service.DTO.DocumentDtos;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarForEditImages
    {
        public Guid CarId { get; set; }

        public string CarName { get; set; }

        public IEnumerable<DocumentBaseInfo> Images { get; set; }
    }
}
