using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;

namespace CarRental.Service.Services
{
    public interface IRentalPointService
    {
        Task Create(RentalPointForCreate rentalPointDto);

        Task<IEnumerable<RentalPointLocation>> GetLocations();

        Task<RentalPointLocation> GetLocation(Guid id);

        Task<IEnumerable<RentalPointTableInfo>> GetTableInfo();

        Task<IEnumerable<string>> GetNames(Guid? id);

        Task<IEnumerable<CarForSmallCard>> GetCars(Guid? id);

        Task Remove(Guid id);

        Task Update(RentalPointForEdit rentalPointDto);
    }
}
