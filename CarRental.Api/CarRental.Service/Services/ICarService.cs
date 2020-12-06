using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.Filter;
using CarRental.Service.WebModels.Car;

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

        Task<IEnumerable<CityBaseInfo>> GetCarsCitiesAsync(Guid countryId);

        Task<IEnumerable<string>> GetCarsMarksAsync();

        Task<PagedCollection<CarForSmallCard>> FilterAndPaginateCars(CarFilterPagingRequest filterPagingRequest);
    }
}
