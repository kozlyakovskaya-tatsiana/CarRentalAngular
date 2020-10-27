using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        private readonly IMapper _mapper;

        private readonly ICarService _carService;

        public CarController(ILogger<CarController> logger, IMapper mapper, ICarService carService)
        {
            _logger = logger;

            _mapper = mapper;

            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var car = new CarDtoBase
            {
                Mark = "Audi",
                Carcase = Carcase.Sedan,
                EnginePower = 4.5,
                FuelConsumption = 20,
                FuelType = FuelType.Diesel,
                Model = "A315",
                ReleaseYear = 2017,
                TankVolume = 40,
                Transmission = Transmission.Mechanical,
                TrunkVolume = 60
            };

            await _carService.CreateCar(car);

            return Ok();
        }
    }
}
