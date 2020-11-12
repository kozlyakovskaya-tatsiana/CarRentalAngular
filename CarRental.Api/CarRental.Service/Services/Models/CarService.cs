using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Exceptions;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarRental.Service.Services.Models
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;

        private readonly IMapper _mapper;

        private readonly ICarRepository _carRepository;

        private readonly IDocumentService _documentService;

        private readonly StaticFilesOptions _staticFilesOptions;

        private readonly IWebHostEnvironment _environment;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository, 
            IRepository<Document> fileRepository, IOptions<StaticFilesOptions> options, IDocumentService documentService,
            IWebHostEnvironment environment)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;

            _staticFilesOptions = options.Value;

            _documentService = documentService;

            _environment = environment;
        }

        public async Task CreateCarAsync(CarCreateDto carCreateDto)
        {
            var carToCreate = _mapper.Map<Car>(carCreateDto);

            carCreateDto.PathToStoreImages = Path.Combine(_environment.WebRootPath, _staticFilesOptions.ImagesStore);

            for (var i = 0; i < carCreateDto.Images.Length; i++)
            {
                _documentService.SetUniqueNameAndPath(carToCreate.Documents[i], carCreateDto.Images[i].FileName, carCreateDto.PathToStoreImages);

                await _documentService.SaveFileInFileSystemAsync(carCreateDto.Images[i], carToCreate.Documents[i].Path);
            }

            await _carRepository.CreateAsync(carToCreate);
        }

        public async Task<IEnumerable<CarReadTableInfoDto>> GetCarsForTableAsync()
        {
            var cars = await _carRepository.GetAsync();

            var carReadDtos = _mapper.Map<IEnumerable<CarReadTableInfoDto>>(cars);

            return carReadDtos;
        }

        public async Task<CarReadWithImagesDto> GetCarWithImagesAsync(Guid id)
        {
            var car = (await _carRepository.GetAllAsync(includes: query => query.Include(c => c.Documents))).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carReadWithImgDto = _mapper.Map<CarReadWithImagesDto>(car);

            carReadWithImgDto.ImageNames = car.Documents.Select(doc => doc.Name).ToArray();

            return carReadWithImgDto;
        }

        public async ValueTask UpdateCarTechInfoAsync(CarInfoDto carTechInfo)
        {
            var car = _mapper.Map<Car>(carTechInfo);

            await _carRepository.UpdateOneAsync(car);
        }

        public async Task AddImagesToCarAsync(CarAddImagesDto carAddImagesDto)
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

        public async Task<CarEditImagesForReadDto> GetCarForEditImagesAsync(Guid id)
        {
            var car = (await _carRepository.GetCarsWithDocuments()).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carForEdt = _mapper.Map<CarEditImagesForReadDto>(car);

            return carForEdt;
        }

        public async Task RemoveCarAsync(Guid id)
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
    }
}
