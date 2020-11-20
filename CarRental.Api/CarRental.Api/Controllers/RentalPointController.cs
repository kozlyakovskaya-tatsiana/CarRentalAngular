using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.Services;
using CarRental.Service.WebModels.RentalPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> CreateRentalPoint(RentalPointCreatingRequest request)
        {
            var rentalPointDto = _mapper.Map<RentalPointCreateDto>(request);

            await _rentalPointService.CreateRentalPoint(rentalPointDto);

            return Ok();
        }
    }
}
