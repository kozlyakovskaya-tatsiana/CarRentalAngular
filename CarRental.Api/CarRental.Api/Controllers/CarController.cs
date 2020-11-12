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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> GetCarForEditImages(Guid id)
        {
            var carForEdit = await _carService.GetCarForEditImagesAsync(id);

            return Ok(carForEdit);
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

        [HttpPost("addimages")]
        public async Task<IActionResult> AddImagesToCar([FromForm] CarAddImagesFormDataRequest carAddImagesFormData)
        {
            var carAddImagesDto = _mapper.Map<CarAddImagesDto>(carAddImagesFormData);

            await _carService.AddImagesToCarAsync(carAddImagesDto);

            return NoContent();
        }

        [HttpPut("info")]
        public async Task<IActionResult> UpdateCarTechInfo([FromBody] CarInfoUpdateRequest request)
        {
            var carDto = _mapper.Map<CarInfoDto>(request);

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
