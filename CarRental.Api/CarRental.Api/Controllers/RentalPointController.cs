using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.Security;
using CarRental.Service.Services;
using CarRental.Service.WebModels.RentalPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForManagersAdmins)]
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
            var info = await _rentalPointService.GetTableInfo();

            return Ok(info);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RentalPointCreateRequest request)
        {
            var rentalPointDto = _mapper.Map<RentalPointForCreate>(request);

            await _rentalPointService.Create(rentalPointDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(RentalPointEditRequest request)
        {
            var pointDto = _mapper.Map<RentalPointForEdit>(request);

            await _rentalPointService.Update(pointDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _rentalPointService.Remove(id);

            return NoContent();
        }
    }
}
