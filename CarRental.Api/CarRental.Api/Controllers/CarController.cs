using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.Helpers;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForManagerOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        private readonly IMapper _mapper;

        private readonly ICarService _carService;

        private readonly ICarHelper _carHelper;

        public CarController(ILogger<CarController> logger, IMapper mapper, ICarService carService, ICarHelper carHelper)
        {
            _logger = logger;

            _mapper = mapper;

            _carService = carService;

            _carHelper = carHelper;
        }

        [HttpGet("carcases")]
        public async Task<IActionResult> GetCarcases()
        {
            var carcases = _carHelper.GetCarcasesTypes();

            return Ok(carcases);
        }

        [HttpGet("fueltypes")]
        public async Task<IActionResult> GetFuelTypes()
        {
            var fuelTypes = _carHelper.GetFuelTypes();

            return Ok(fuelTypes);
        }

        [HttpGet("transmissionstypes")]
        public async Task<IActionResult> GetTransmissionTypes()
        {
            var transmissionTypes = _carHelper.GetTransmissionTypes();

            return Ok(transmissionTypes);
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _carService.GetCars();

            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarCreatingRequest carCreatingRequest)
        {
            var carDtoBase = _mapper.Map<CarDtoBase>(carCreatingRequest);

            await _carService.CreateCar(carDtoBase);

            return Ok();
        }
    }
}
