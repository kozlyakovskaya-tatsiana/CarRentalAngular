using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Api.Security;
using CarRental.DAL.Entities;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForUserOnly)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> BookCar([FromBody] BookingRequest bookingRequest)
        {
            await _bookingService.BookCarAsync(bookingRequest);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Bookings()
        {
            return Ok();
        }
    }
}
