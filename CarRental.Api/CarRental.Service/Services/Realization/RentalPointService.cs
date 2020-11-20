using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.RentalPointDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRental.Service.Services.Realization
{
    public class RentalPointService : IRentalPointService
    {
        private readonly ILogger<RentalPointService> _logger;

        private readonly IMapper _mapper;

        private readonly IRentalPointRepository _rentalPointRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ILocationRepository _locationRepository;

        public RentalPointService(ILogger<RentalPointService> logger, IMapper mapper, IRentalPointRepository rentalPointRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository, ILocationRepository locationRepository)
        {
            _logger = logger;

            _mapper = mapper;

            _rentalPointRepository = rentalPointRepository;

            _countryRepository = countryRepository;

            _cityRepository = cityRepository;

            _locationRepository = locationRepository;
        }

        public async Task CreateRentalPoint(RentalPointCreateDto rentalPointDto)
        {
            var rentalPoint = _mapper.Map<RentalPoint>(rentalPointDto);

            var existingCountry = await _countryRepository.GetCountryByNameAsync(rentalPointDto.Country);

            var existingCity = await _cityRepository.GetCityByNameAsync(rentalPointDto.City);

            var existingLocation = await _locationRepository.GetLocationByAddressAsync(rentalPointDto.Address);
            
            rentalPoint.Location.City.Country = existingCountry ?? rentalPoint.Location.City.Country;

            rentalPoint.Location.City = existingCity ?? rentalPoint.Location.City;

            rentalPoint.Location = existingLocation ?? rentalPoint.Location;

            await _rentalPointRepository.CreateAsync(rentalPoint);
        }

        public async Task<IEnumerable<RentalPoint>> GetRentalPointsLocations()
        {
            return await _rentalPointRepository.GetRentalPointsWithLocations();
        }
    }
}
