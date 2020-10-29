﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Enums;
using CarRental.Service.DTO.CarDtos;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateCar(CarDtoBase carDtoBase);

        Task<IEnumerable<CarReadDto>> GetCars();

    }
}