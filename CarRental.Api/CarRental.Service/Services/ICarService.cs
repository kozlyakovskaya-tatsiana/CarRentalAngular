using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateAsync(CarForCreate carCreateDto);

        Task<IEnumerable<CarTableInfo>> GetCarsTableInfoAsync();

        Task<CarWithImages> GetCarWithImagesAsync(Guid id);

        Task RemoveAsync(Guid id);

        ValueTask UpdateCarTechInfoAsync(CarInfo carTechInfo);

        Task<CarForEditImages> GetCarForEditImagesAsync(Guid id);

        Task AddImagesToCarAsync(CarForAddImages carAddImagesDto);

        Task<IEnumerable<CarForSmallCard>> GetCarsForSmallCardsAsync();
    }
}
