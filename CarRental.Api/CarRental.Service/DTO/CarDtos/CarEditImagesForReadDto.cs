using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Service.DTO.DocumentsDto;

namespace CarRental.Service.DTO.CarDtos
{
    public class CarEditImagesForReadDto
    {
        public Guid CarId { get; set; }

        public string CarName { get; set; }

        public IEnumerable<DocumentDto> Images { get; set; }
    }
}
