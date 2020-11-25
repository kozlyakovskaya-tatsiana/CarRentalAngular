using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetRentalPointsLocations()
        {
            var locationsInfo = await _rentalPointService.GetRentalPointsLocations();

            return Ok(locationsInfo);
        }

        [HttpGet("location/{id}")]
        public async Task<IActionResult> GetRentalPointLocation(Guid id)
        {
            var rentalPoint = await _rentalPointService.GetRentalPointLocation(id);

            return Ok(rentalPoint);
        }

        [HttpGet("cars/{id?}")]
        public async Task<IActionResult> GetCarsOfRentalPoint(Guid? id = null)
        {
            var cars = await _rentalPointService.GetCarsOfRentalPoint(id);

            return Ok(cars);
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetPointsNames()
        {
            var names = await _rentalPointService.GetRentalPointNames();

            return Ok(names);
        }
    }
}
