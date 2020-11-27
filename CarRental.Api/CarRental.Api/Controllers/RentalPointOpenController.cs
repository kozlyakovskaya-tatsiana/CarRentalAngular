using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/rentalpoint")]
    [ApiController]
    public class RentalPointOpenController : ControllerBase
    {
        private readonly ILogger<RentalPointController> _logger;

        private readonly IMapper _mapper;

        private readonly IRentalPointService _rentalPointService;

        public RentalPointOpenController(ILogger<RentalPointController> logger, IMapper mapper, IRentalPointService rentalPointService)
        {
            _logger = logger;

            _mapper = mapper;

            _rentalPointService = rentalPointService;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var locationsInfo = await _rentalPointService.GetLocations();

            return Ok(locationsInfo);
        }

        [HttpGet("location/{id}")]
        public async Task<IActionResult> GetLocation(Guid id)
        {
            var rentalPoint = await _rentalPointService.GetLocation(id);

            return Ok(rentalPoint);
        }

        [HttpGet("cars/{id?}")]
        public async Task<IActionResult> GetCars(Guid? id = null)
        {
            var cars = await _rentalPointService.GetCars(id);

            return Ok(cars);
        }

        [HttpGet("names/{id?}")]
        public async Task<IActionResult> GetNames(Guid? id = null)
        {
            var names = await _rentalPointService.GetNames(id);

            return Ok(names);
        }
    }
}
