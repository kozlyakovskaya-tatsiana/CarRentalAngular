using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.Services;
using CarRental.Service.WebModels.RentalPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForManagersAdmins")]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalPointController : ControllerBase
    {
        private readonly ILogger<RentalPointController> _logger;

        private readonly IMapper _mapper;

        private readonly IRentalPointService _rentalPointService;

        public RentalPointController(ILogger<RentalPointController> logger, IMapper mapper, IRentalPointService rentalPointService)
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

        [HttpGet("tableinfo")]
        public async Task<IActionResult> GetTableInfo()
        {
            var info = await _rentalPointService.GetRentalPointsTableInfo();

            return Ok(info);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentalPoint(RentalPointCreatingRequest request)
        {
            var rentalPointDto = _mapper.Map<RentalPointCreateDto>(request);

            await _rentalPointService.CreateRentalPoint(rentalPointDto);

            return Ok();
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetPointsNames()
        {
            var names = await _rentalPointService.GetRentalPointNames();

            return Ok(names);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRentalPoint(Guid id)
        {
            await _rentalPointService.RemoveRentalPoint(id);

            return NoContent();
        }
    }
}
