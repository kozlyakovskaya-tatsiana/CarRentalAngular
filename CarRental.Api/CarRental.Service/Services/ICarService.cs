﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(CarCreateDto carCreateDto);

        Task<IEnumerable<CarReadDto>> GetCarsAsync();

        Task<CarReadWithImageDto> GetCarWithImgsAsync(Guid id);

        Task RemoveCarAsync(Guid id);

        Task<CarReadDto> GetCarAsync(Guid id);
    }
}
