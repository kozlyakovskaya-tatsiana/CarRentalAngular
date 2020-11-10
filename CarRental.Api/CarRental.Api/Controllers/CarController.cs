using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.Helpers;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForManagersAdmins")]
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

        /// <summary>
        /// Get car's carcases.
        /// </summary>
        /// <returns>Array of carcases.</returns>
        /// <response code="200">Returns carcases.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpGet("carcases")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCarcases()
        {
            var carcases = _carHelper.GetCarcasesTypes();

            return Ok(carcases);
        }

        /// <summary>
        /// Get types of fuel.
        /// </summary>
        /// <returns>Array of fuel's types.</returns>
        /// <response code="200">Returns types of fuel.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpGet("fueltypes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetFuelTypes()
        {
            var fuelTypes = _carHelper.GetFuelTypes();

            return Ok(fuelTypes);
        }

        /// <summary>
        /// Get transmission's types.
        /// </summary>
        /// <returns>Array of transmission types.</returns>
        /// <response code="200">Returns transmission's types.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpGet("transmissionstypes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetTransmissionTypes()
        {
            var transmissionTypes = _carHelper.GetTransmissionTypes();

            return Ok(transmissionTypes);
        }

        /// <summary>
        /// Get types of status.
        /// </summary>
        /// <returns>Array of status types.</returns>
        /// <response code="200">Returns types of status.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpGet("statustypes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetStatusTypes()
        {
            var statusTypes = _carHelper.GetStatusTypes();

            return Ok(statusTypes);
        }

        /// <summary>
        /// Get an array of cars.
        /// </summary>
        /// <returns>An array of cars.</returns>
        /// <response code="200">Returns cars.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpGet("cars")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCarsForTable()
        {
            var cars = await _carService.GetCarsForTableAsync();

            return Ok(cars);
        }

        /// <summary>
        /// Get car by id.
        /// </summary>
        /// <param name="id">Car's id.</param>
        /// <returns>Car</returns>
        /// <response code="200">Returns car.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">Car with such id is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCarWithImages(Guid id)
        {
            var car = await _carService.GetCarWithImagesAsync(id);

            return Ok(car);
        }

        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetCarImages(Guid id)
        {
            var images = await _carService.GetCarsImages(id);

            return Ok(images);
        }

        /// <summary>
        /// Create a car.
        /// </summary>
        /// <param name="carCreatingFormData">Data for creating car.</param>
        /// <returns>Result of creating.</returns>
        /// <response code="200">Creating is successful.</response>
        /// <response code="400">Validation is failed.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateCar([FromForm] CarCreatingFormDataRequest carCreatingFormData)
        {
            var carCreatingDto = _mapper.Map<CarCreateDto>(carCreatingFormData);

            await _carService.CreateCarAsync(carCreatingDto);

            return Ok();
        }

        [HttpPut("techinfo")]
        public async Task<IActionResult> UpdateCarTechInfo([FromBody] CarTechInfoUpdateRequest request)
        {
            var carDto = _mapper.Map<CarTechInfoDto>(request);

            await _carService.UpdateCarTechInfoAsync(carDto);

            return NoContent();
        }

        /// <summary>
        /// Remove car by id.
        /// </summary>
        /// <param name="id">Car's id.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Deleting is successful.</response>
        /// <response code="401">Access denied. Authorization failed.</response>
        /// <response code="404">Car with such id is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveCar(Guid id)
        {
            await _carService.RemoveCarAsync(id);

            return Ok();
        }
    }
}
