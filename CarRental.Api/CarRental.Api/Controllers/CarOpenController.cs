using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.Helpers;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/car")]
    [ApiController]
    public class CarOpenController : ControllerBase
    {
        private readonly ICarService _carService;

        private readonly ICarHelper _carHelper;

        public CarOpenController(ILogger<CarOpenController> logger, IMapper mapper, ICarService carService, ICarHelper carHelper)
        {
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
        public IActionResult GetCarcases()
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
        public IActionResult GetFuelTypes()
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
        public IActionResult GetTransmissionTypes()
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
        public IActionResult GetStatusTypes()
        {
            var statusTypes = _carHelper.GetStatusTypes();

            return Ok(statusTypes);
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

        [HttpGet("carsforsmallcards")]
        public async Task<IActionResult> GetCarsForSmallCards()
        {
            var cars = await _carService.GetCarsForSmallCardsAsync();

            return Ok(cars);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCarsCountries()
        {
            var countries = await _carService.GetCarsCountriesAsync();

            return Ok(countries);
        }

        [HttpGet("countries/{id}/cities")]
        public async Task<IActionResult> GetCarsCities(Guid id)
        {
            var cities = await _carService.GetCarsCitiesAsync(id);

            return Ok(cities);
        }

        [HttpGet("cities/{id}/points")]
        public async Task<IActionResult> GetCarsPoints(Guid id)
        {
            var points = await _carService.GetCarsRentalPointsAsync(id);

            return Ok(points);
        }

        [HttpGet("marks")]
        public async Task<IActionResult> GetCarMarks()
        {
            var marks = await _carService.GetCarsMarksAsync();

            return Ok(marks);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterCars([FromQuery]CarFilterPagingRequest filterPagingRequest)
        {
            var response = await _carService.FilterAndPaginateCars(filterPagingRequest);

            return Ok(response);
        }
    }
}
