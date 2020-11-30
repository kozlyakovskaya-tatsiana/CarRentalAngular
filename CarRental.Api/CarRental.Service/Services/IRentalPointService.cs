using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.RentalPointDtos;

namespace CarRental.Service.Services
{
    public interface IRentalPointService
    {
        Task CreateRentalPoint(RentalPointCreateDto rentalPointDto);

        Task<IEnumerable<RentalPointLocationDto>> GetRentalPointsLocations();

        Task<RentalPointLocationDto> GetRentalPointLocation(Guid id);

        Task<IEnumerable<RentalPointTableInfoDto>> GetRentalPointsTableInfo();

        Task<IEnumerable<string>> GetRentalPointNames(Guid? id);

        Task<IEnumerable<CarForSmallCardDto>> GetCarsOfRentalPoint(Guid? id);

        Task RemoveRentalPoint(Guid id);

        Task UpdateRentalPoint(RentalPointEditDto rentalPointDto);
    }
}
