using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.RentalPointDtos;

namespace CarRental.Service.Services
{
    public interface IRentalPointService
    {
        Task CreateRentalPoint(RentalPointCreateDto rentalPointDto);

        Task<IEnumerable<RentalPointLocationsDto>> GetRentalPointsLocations();

        Task<RentalPointLocationsDto> GetRentalPointLocation(Guid id);

        Task<IEnumerable<RentalPointTableInfoDto>> GetRentalPointsTableInfo();

        Task<IEnumerable<string>> GetRentalPointNames();

        Task RemoveRentalPoint(Guid id);
    }
}
