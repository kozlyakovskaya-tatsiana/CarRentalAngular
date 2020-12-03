using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;

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

        Task<IEnumerable<CountryBaseInfo>> GetCarsCountriesAsync();

        Task<IEnumerable<string>> GetCarsCitiesAsync(Guid countryId);
    }
}
