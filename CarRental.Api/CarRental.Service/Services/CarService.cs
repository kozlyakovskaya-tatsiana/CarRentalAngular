﻿using System;
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
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CarRental.Service.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;

        private readonly IMapper _mapper;

        private readonly IRepository<Car> _carRepository;

        private readonly IConfiguration _configuration;

        public CarService(ILogger<CarService> logger, IMapper mapper, IRepository<Car> carRepository, IRepository<Document> fileRepository, IConfiguration configuration)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;

            _configuration = configuration;
        }

        public async Task CreateCarAsync(CarCreateDto carCreateDto )
        {
            var carToCreate = _mapper.Map<Car>(carCreateDto);

            carCreateDto.PathToStoreImages = _configuration["ImagesStoreFolder"];

            for (var i = 0; i < carCreateDto.Images.Length; i++)
            {
                var imageName = Guid.NewGuid() + carCreateDto.Images[i].FileName;

                var imagePath = carCreateDto.PathToStoreImages + imageName;

                await SaveFileInFileSystemAsync(carCreateDto.Images[i], imagePath);

                carToCreate.Documents[i].Path = imagePath;

                carToCreate.Documents[i].Name = imageName;
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

            carReadWithImgDto.ImageNames = car.Documents.Select(doc => doc.Name).ToArray();

            return carReadWithImgDto;
        }

        public async Task RemoveCarAsync(Guid id)
        {
            var car = _carRepository.Include(c => c.Documents).FirstOrDefault(c => c.Id == id);

            if (car == null)
                throw new NotFoundException("There is no car with such Id");

            var images = car.Documents.Select(doc => doc.Path);

            foreach (var image in images)
            {
                File.Delete(image);
            }

            await _carRepository.RemoveAsync(id);

            await _carRepository.SaveChangesAsync();
        }
    }
}
