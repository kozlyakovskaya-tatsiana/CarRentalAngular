using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
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

        public CarService(ILogger<CarService> logger, IMapper mapper, IRepository<Car> carRepository, IRepository<ImageFile> fileRepository)
        {
            _logger = logger;

            _mapper = mapper;

            _carRepository = carRepository;
        }

        public async Task CreateCarAsync(CarCreateDto carCreateDto)
        {
            var carToCreate = _mapper.Map<Car>(carCreateDto);

            carToCreate.MainImage.Name = carCreateDto.MainImageFile.FileName;

            using (var ms = new MemoryStream())
            {
                await carCreateDto.MainImageFile.CopyToAsync(ms);

                carToCreate.MainImage.ImageDataUrl = ms.ToArray();
            }

            await _carRepository.CreateAsync(carToCreate);

            await _carRepository.SaveChangesAsync();

            /*var imageFile = "/Files/" + carCreateDto.MainImageFile.FileName;

             var path = Path.Join(AppContext.BaseDirectory, imageFile);*/

            //carToCreate.MainImageFile.Path = path;

            // await SaveFileInFileSystemAsync(carCreateDto.MainImageFile, path);
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

        public async Task<CarReadWithImageDto> GetCarWithImgsAsync(Guid id)
        {
            var car = _carRepository.Include(c => c.MainImage).FirstOrDefault(c => c.Id == id);

            var carReadWithImgDto = _mapper.Map<CarReadWithImageDto>(car);

            return carReadWithImgDto;
        }

        public async Task RemoveCarAsync(Guid id)
        {
            await _carRepository.RemoveAsync(id);

            await _carRepository.SaveChangesAsync();
        }
    }
}
