using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using CarRental.Service.DTO.CarDtos;
using Microsoft.Extensions.Logging;

namespace CarRental.Service.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;

        private readonly IMapper _mapper;

        private readonly IRepository<Car> _carRepository;

        public CarService(ILogger<CarService> logger, IMapper mapper, IRepository<Car> carRepository)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;
        }

        public async Task CreateCar(CarDtoBase carDtoBase)
        {
            var carToCreate = _mapper.Map<Car>(carDtoBase);

            await _carRepository.CreateAsync(carToCreate);

            await _carRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarReadDto>> GetCars()
        {
            var cars = await _carRepository.GetAsync();

            var carReadDtos = _mapper.Map<IEnumerable<CarReadDto>>(cars);

            return carReadDtos;
        }

        public IEnumerable<string> GetCarcasesTypes()
        {
            var carcases = Enum.GetValues(typeof(CarcaseType)).Cast<CarcaseType>().Select(type => type.ToString());

            return carcases;
        }

        public IEnumerable<string> GetFuelTypes()
        {
            var fuelTypes = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Select(type => type.ToString());

            return fuelTypes;
        }

        public IEnumerable<string> GetTransmissionTypes()
        {
            var transmissionTypes = Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>().Select(type => type.ToString());

            return transmissionTypes;
        }

    }
}
