using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Exceptions;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;
using Microsoft.EntityFrameworkCore;


namespace CarRental.Service.Services.Realization
{
    public class RentalPointService : IRentalPointService
    {
        private readonly IMapper _mapper;

        private readonly IRentalPointRepository _rentalPointRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ILocationRepository _locationRepository;

        public RentalPointService(IMapper mapper, IRentalPointRepository rentalPointRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository, ILocationRepository locationRepository)
        {
            _mapper = mapper;

            _rentalPointRepository = rentalPointRepository;

            _countryRepository = countryRepository;

            _cityRepository = cityRepository;

            _locationRepository = locationRepository;
        }

        public async Task Create(RentalPointForCreate rentalPointDto)
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

        public async Task<IEnumerable<CarForSmallCard>> GetCars(Guid? id)
        {
            var cars = id == null
                    ? (await _rentalPointRepository.GetAllAsync(includes:
                        p => p.Include(p => p.Cars).ThenInclude(car => car.Documents)))
                    .SelectMany(p => p.Cars)
                    : (await _rentalPointRepository.GetAllAsync(p => p.Id ==id,
                        p => p.Include(p => p.Cars).ThenInclude(car => car.Documents)))
                    .SelectMany(p => p.Cars);

            var carsForCards = _mapper.Map<IEnumerable<CarForSmallCard>>(cars);

            return carsForCards;
        }

        public async Task<RentalPointLocation> GetLocation(Guid id)
        {
            var point = (await _rentalPointRepository.GetRentalPointsWithLocations(p => p.Id == id)).FirstOrDefault();

            var pointDto = _mapper.Map<RentalPointLocation>(point);

            return pointDto;
        }

        public async Task<IEnumerable<string>> GetNames(Guid? id)
        {
            var names = id == null
                ? (await _rentalPointRepository.GetAsync()).Select(p => p.Name)
                : (await _rentalPointRepository.GetAsync(p => p.Id == id)).Select(p => p.Name);

            return names;
        }

        public async Task<IEnumerable<RentalPointLocation>> GetLocations()
        {
            var points = await _rentalPointRepository.GetRentalPointsWithLocations();

            var pointsDto = _mapper.Map<IEnumerable<RentalPointLocation>>(points);

            return pointsDto;
        }

        public async Task<IEnumerable<RentalPointTableInfo>> GetTableInfo()
        {
            var points = await _rentalPointRepository.GetAllAsync(includes:
                point => point.Include(p => p.Location.City.Country)
                    .Include(p => p.Cars));

            var info = _mapper.Map<IEnumerable<RentalPointTableInfo>>(points);

            return info;
        }

        public async Task Remove(Guid id)
        {
            var point = await _rentalPointRepository.FindByIdAsync(id);

            if (point == null)
                throw new NotFoundException($"No rental point with id={id}");

            await _rentalPointRepository.RemoveAsync(id);
        }

        public async Task Update(RentalPointForEdit rentalPointDto)
        {
            var point = _mapper.Map<RentalPoint>(rentalPointDto);

            var existingCountry = await _countryRepository.GetCountryByNameAsync(rentalPointDto.Country);

            var existingCity = await _cityRepository.GetCityByNameAsync(rentalPointDto.City);

            var existingLocation = await _locationRepository.GetLocationByAddressAsync(rentalPointDto.Address);

            point.Location.City.Country = existingCountry ?? point.Location.City.Country;

            point.Location.City = existingCity ?? point.Location.City;

            point.Location = existingLocation ?? point.Location;

            await _rentalPointRepository.UpdateOneAsync(point);
        }
    }
}
