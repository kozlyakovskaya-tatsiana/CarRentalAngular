using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Exceptions;
using CarRental.Service.DTO.CarDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRental.Service.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;

        private readonly IMapper _mapper;

        private readonly IRepository<Car> _carRepository;

        public CarService(ILogger<CarService> logger, IMapper mapper, IRepository<Car> carRepository, IRepository<Document> fileRepository)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;
        }

        public async Task CreateCarAsync(CarCreateDto carCreateDto)
        {
            var carToCreate = _mapper.Map<Car>(carCreateDto);

            for (int i = 0; i < carCreateDto.Images.Length; i++)
            {
                var imagePath = carCreateDto.PathToStoreImages + carToCreate.Id + carCreateDto.Images[i].FileName;

                await SaveFileInFileSystemAsync(carCreateDto.Images[i], imagePath);

                carToCreate.Documents[i].Path = imagePath;
            }

            await _carRepository.CreateAsync(carToCreate);

            await _carRepository.SaveChangesAsync();
        }

        private async Task SaveFileInFileSystemAsync(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task<IEnumerable<CarReadDto>> GetCarsAsync()
        {
            var cars = await _carRepository.GetAsync();

            var carReadDtos = _mapper.Map<IEnumerable<CarReadDto>>(cars);

            return carReadDtos;
        }

        public async Task<CarReadDto> GetCarAsync(Guid id)
        {
            var car = await _carRepository.FindByIdAsync(id);

            var carReadDto = _mapper.Map<CarReadDto>(car);

            return carReadDto;
        }

        public async Task<CarReadWithImageDto> GetCarWithImagesAsync(Guid id)
        {
            var car = _carRepository.Include(c => c.Documents).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var carReadWithImgDto = _mapper.Map<CarReadWithImageDto>(car);

            carReadWithImgDto.ImageDataUrls = new string[car.Documents.Count];

            for (int i = 0; i < car.Documents.Count; i++)
            {
                var imgDataBytes = await File.ReadAllBytesAsync(car.Documents[i].Path);

                carReadWithImgDto.ImageDataUrls[i] = $"data:{car.Documents[i].Type};base64,{Convert.ToBase64String(imgDataBytes)}";
            }

            return carReadWithImgDto;
        }

        public async Task RemoveCarAsync(Guid id)
        {
            await _carRepository.RemoveAsync(id);

            await _carRepository.SaveChangesAsync();
        }
    }
}
