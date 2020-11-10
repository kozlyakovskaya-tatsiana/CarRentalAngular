using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(CarCreateDto carCreateDto);

        Task<IEnumerable<CarReadDto>> GetCarsAsync();

        Task<CarReadWithImageDto> GetCarWithImagesAsync(Guid id);

        Task RemoveCarAsync(Guid id);

        Task<CarReadDto> GetCarReadDtoAsync(Guid id);

    }
}
