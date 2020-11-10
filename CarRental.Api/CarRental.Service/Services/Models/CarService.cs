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
using CarRental.Service.Identity.Options;
using CarRental.Service.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
            var carToCreate = _mapper.Map<Car>(carCreateDto); ;

            carCreateDto.PathToStoreImages = Path.Combine(_environment.WebRootPath, _staticFilesOptions.ImagesStore);

            for (var i = 0; i < carCreateDto.Images.Length; i++)
            {
                var imageName = Guid.NewGuid() + carCreateDto.Images[i].FileName;

                var imagePath = Path.Combine(carCreateDto.PathToStoreImages,imageName);

                await _documentService. SaveFileInFileSystemAsync(carCreateDto.Images[i], imagePath);

                carToCreate.Documents[i].Path = imagePath;

                carToCreate.Documents[i].Name = imageName;
            }

            await _carRepository.CreateAsync(carToCreate);
        }

        public async Task<IEnumerable<CarReadDto>> GetCarsAsync()
        {
            var cars = await _carRepository.GetAsync();

            var carReadDtos = _mapper.Map<IEnumerable<CarReadDto>>(cars);

            return carReadDtos;
        }

        public async Task<CarReadDto> GetCarReadDtoAsync(Guid id)
        {
            var car = await _carRepository.FindByIdAsync(id);

            var carReadDto = _mapper.Map<CarReadDto>(car);

            return carReadDto;
        }

        public async Task<CarReadWithImageDto> GetCarWithImagesAsync(Guid id)
        {
            var car = (await _carRepository.GetAllAsync(includes: query => query.Include(c => c.Documents))).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carReadWithImgDto = _mapper.Map<CarReadWithImageDto>(car);

            carReadWithImgDto.ImageNames = car.Documents.Select(doc => doc.Name).ToArray();

            return carReadWithImgDto;
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
