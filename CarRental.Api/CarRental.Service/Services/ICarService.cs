using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(CarCreateDto carCreateDto);

        Task<IEnumerable<CarReadTableInfoDto>> GetCarsForTableAsync();

        Task<CarReadWithImagesDto> GetCarWithImagesAsync(Guid id);

        Task RemoveCarAsync(Guid id);

        ValueTask UpdateCarTechInfoAsync(CarInfoDto carTechInfo);

        Task<CarEditImagesForReadDto> GetCarForEditImagesAsync(Guid id);

        Task AddImagesToCarAsync(CarAddImagesDto carAddImagesDto);

        Task<IEnumerable<CarForSmallCardDto>> GetCarsForSmallCardsAsync();
    }
}
