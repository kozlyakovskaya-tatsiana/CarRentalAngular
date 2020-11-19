using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.RentalPointDtos;
using Microsoft.Extensions.Logging;
using System.Linq;
using CarRental.DAL.Repositories;

namespace CarRental.Service.Services.Models
{
    public class RentalPointService : IRentalPointService
    {
        private readonly ILogger<RentalPointService> _logger;

        private readonly IMapper _mapper;

        private readonly IRepository<RentalPoint> _rentalPointRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ILocationRepository _locationRepository;

        public RentalPointService(ILogger<RentalPointService> logger, IMapper mapper, IRepository<RentalPoint> rentalPointRepository,
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

            // add checking if this address is already exists
        }
    }
}
