using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Service.DTO.CarDtos;

namespace CarRental.Service.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(CarDtoBase carDtoBase);

        Task<IEnumerable<CarReadDto>> GetCarsAsync();

        Task RemoveCarAsync(int id);

        Task<CarReadDto> GetCarAsync(int id);

    }
}
