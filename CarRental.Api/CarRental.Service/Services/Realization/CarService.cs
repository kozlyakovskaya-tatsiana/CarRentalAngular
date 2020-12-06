using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Exceptions;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.Filter;
using CarRental.Service.Options;
using CarRental.Service.WebModels.Car;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarRental.Service.Services.Realization
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;

        private readonly IMapper _mapper;

        private readonly ICarRepository _carRepository;

        private readonly IRentalPointRepository _rentalPointRepository;

        private readonly IDocumentService _documentService;

        private readonly StaticFilesOptions _staticFilesOptions;

        private readonly IWebHostEnvironment _environment;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository, 
            IRepository<Document> fileRepository, IOptions<StaticFilesOptions> options, IDocumentService documentService,
            IWebHostEnvironment environment, IRentalPointRepository rentalPointRepository)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;

            _staticFilesOptions = options.Value;

            _documentService = documentService;

            _environment = environment;

            _rentalPointRepository = rentalPointRepository;
        }

        public async Task CreateAsync(CarForCreate carCreateDto)
        {
            var carToCreate = _mapper.Map<Car>(carCreateDto);

            var rentalPoint = await _rentalPointRepository.GetRentalPointByNameAsync(carCreateDto.RentalPointName);

            if(rentalPoint==null)
                throw new NotFoundException($"RentalPoint with name {carCreateDto.RentalPointName} is not found.");

            carToCreate.RentalPointId = rentalPoint.Id;

            carCreateDto.PathToStoreImages = Path.Combine(_environment.WebRootPath, _staticFilesOptions.ImagesStore);

            for (var i = 0; i < carCreateDto.Images.Length; i++)
            {
                _documentService.SetUniqueNameAndPath(carToCreate.Documents[i], carCreateDto.Images[i].FileName, carCreateDto.PathToStoreImages);

                await _documentService.SaveFileInFileSystemAsync(carCreateDto.Images[i], carToCreate.Documents[i].Path);
            }

            await _carRepository.CreateAsync(carToCreate);
        }

        public async Task<IEnumerable<CarTableInfo>> GetCarsTableInfoAsync()
        {
            var cars = await _carRepository.GetAllAsync(includes: car => car.Include(c => c.RentalPoint));

            var carReadDtos = _mapper.Map<IEnumerable<CarTableInfo>>(cars);

            return carReadDtos;
        }

        public async Task<CarWithImages> GetCarWithImagesAsync(Guid id)
        {
            var car = (await _carRepository.GetAllAsync(includes: query => query
                .Include(c => c.Documents)
                .Include(c => c.RentalPoint)))
                .FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carReadWithImgDto = _mapper.Map<CarWithImages>(car);

            carReadWithImgDto.ImageNames = car.Documents.Select(doc => doc.Name).ToArray();

            return carReadWithImgDto;
        }

        public async Task<IEnumerable<CarForSmallCard>> GetCarsForSmallCardsAsync()
        {
            var cars = await _carRepository.GetCarsWithDocuments();

            var carsForSmallCards = _mapper.Map<IEnumerable<CarForSmallCard>>(cars);

            return carsForSmallCards;
        }

        public async ValueTask UpdateCarTechInfoAsync(CarInfo carTechInfo)
        {
            var car = _mapper.Map<Car>(carTechInfo);

            var rentalPoint = await _rentalPointRepository.GetRentalPointByNameAsync(carTechInfo.RentalPointName);

            if(rentalPoint==null)
                throw new NotFoundException($"Rental point ${carTechInfo.RentalPointName} is not found.");

            car.RentalPoint = rentalPoint;

            await _carRepository.UpdateOneAsync(car);
        }

        public async Task AddImagesToCarAsync(CarForAddImages carAddImagesDto)
        {
            var car = (await _carRepository.GetCarsWithDocuments()).FirstOrDefault(c => c.Id == carAddImagesDto.CarId);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var pathToStoreImages = Path.Combine(_environment.WebRootPath, _staticFilesOptions.ImagesStore);

            var documentsToAdd = _mapper.Map<Document[]>(carAddImagesDto.Images);

            for (var i = 0; i < carAddImagesDto.Images.Length; i++)
            {
                var document = documentsToAdd[i];

                _documentService.SetUniqueNameAndPath(document, carAddImagesDto.Images[i].FileName, pathToStoreImages);

                car.Documents.Add(document);

                await _documentService.SaveFileInFileSystemAsync(carAddImagesDto.Images[i], document.Path);
            }

            await _carRepository.UpdateOneAsync(car);
        }

        public async Task<CarForEditImages> GetCarForEditImagesAsync(Guid id)
        {
            var car = (await _carRepository.GetCarsWithDocuments()).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carForEdt = _mapper.Map<CarForEditImages>(car);

            return carForEdt;
        }

        public async Task RemoveAsync(Guid id)
        {
            var car = (await _carRepository.GetCarsWithDocuments()).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var images = car.Documents.Select(doc => doc.Path);

            foreach (var image in images)
            {
                File.Delete(image);
            }

            await _carRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<CountryBaseInfo>> GetCarsCountriesAsync()
        {
            var countries = await (await _carRepository.GetAllAsync(includes: car => car
                .Include(c => c.RentalPoint.Location.City.Country)))
                .Select(c => c.RentalPoint.Location.City.Country)
                .Distinct()
                .ToArrayAsync();

            var countriesInfo = _mapper.Map<IEnumerable<CountryBaseInfo>>(countries);

            return countriesInfo;
        }

        public async Task<IEnumerable<CityBaseInfo>> GetCarsCitiesAsync(Guid countryId)
        {
            var cities = await (await _carRepository.GetAllAsync(includes: car => car
                    .Include(c => c.RentalPoint.Location.City.Country)))
                .Where(c => c.RentalPoint.Location.City.CountryId == countryId)
                .Select(c => c.RentalPoint.Location.City)
                .Distinct()
                .ToArrayAsync();

            var citiesInfo = _mapper.Map<IEnumerable<CityBaseInfo>>(cities);

            return citiesInfo;
        }

        public async Task<IEnumerable<string>> GetCarsMarksAsync()
        {
            var marks = (await _carRepository.GetAllAsync()).Select(c => c.Mark).Distinct();

            return marks;
        }

        public async Task<PagedCollection<CarForSmallCard>> FilterAndPaginateCars(CarFilterPagingRequest filterPagingRequest)
        {
            Expression<Func<Car, bool>> carFiltering = car =>
                (filterPagingRequest.CountryId == null ? true : car.RentalPoint.Location.City.CountryId == filterPagingRequest.CountryId) &&
                (filterPagingRequest.CityId == null ? true : car.RentalPoint.Location.CityId == filterPagingRequest.CityId) &&
                (filterPagingRequest.Marks == null ? true : filterPagingRequest.Marks.Contains(car.Mark)) &&
                (filterPagingRequest.Transmissions == null ? true : filterPagingRequest.Transmissions.Contains(car.Transmission)) &&
                (filterPagingRequest.Carcases == null ? true : filterPagingRequest.Carcases.Contains(car.Carcase));

            var cars = await _carRepository.GetAllAsync(carFiltering);

            var totalCars = await cars.CountAsync();
            var totalpages = (int)Math.Ceiling(totalCars / (double)filterPagingRequest.PageSize);

            var carsPerPage = await cars.Skip((filterPagingRequest.PageNumber - 1) * filterPagingRequest.PageSize)
                .Take(filterPagingRequest.PageSize)
                .ToArrayAsync();

            var carsToShow = _mapper.Map<IEnumerable<CarForSmallCard>>(carsPerPage);

            var result = new PagedCollection<CarForSmallCard>(carsToShow, filterPagingRequest.PageNumber, totalpages);

            return result;
        }
    }
}
