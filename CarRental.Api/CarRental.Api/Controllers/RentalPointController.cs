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

        [HttpGet("tableinfo")]
        public async Task<IActionResult> GetTableInfo()
        {
            var info = await _rentalPointService.GetRentalPointsTableInfo();

            return Ok(info);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentalPoint(RentalPointCreateRequest request)
        {
            var rentalPointDto = _mapper.Map<RentalPointCreateDto>(request);

            await _rentalPointService.CreateRentalPoint(rentalPointDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRentalPoint(RentalPointEditRequest request)
        {
            var pointDto = _mapper.Map<RentalPointEditDto>(request);

            await _rentalPointService.UpdateRentalPoint(pointDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRentalPoint(Guid id)
        {
            await _rentalPointService.RemoveRentalPoint(id);

            return NoContent();
        }
    }
}
