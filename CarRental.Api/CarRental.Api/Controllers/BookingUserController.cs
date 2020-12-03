using System;
using System.Threading.Tasks;
using CarRental.Api.Security;
using CarRental.DAL.Enums;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForUserOnly)]
    [Route("api/booking")]
    [ApiController]
    public class BookingUserController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingUserController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookCar([FromBody] BookingRequest bookingRequest)
        {
            await _bookingService.BookCarAsync(bookingRequest);

            return Ok();
        }

        [HttpGet("userreject/{id}")]
        public async Task<IActionResult> RejectBooking(Guid id)
        {
            await _bookingService.RejectBookingByUserAsync(id);

            return Ok();
        }

        [HttpGet("user/{id}/list")]
        public async Task<IActionResult> GetAllUserBookings(string id)
        {
            var bookings = await _bookingService.GetAllUserBookings(id);

            return Ok(bookings);
        }

        [HttpGet("user/{id}/list/{status}")]
        public async Task<IActionResult> GetAllUserBookingsByStatus(string id, BookingStatus status)
        {
            var bookings = await _bookingService.GetUserBookingsByStatusAsync(id, status);

            return Ok(bookings);
        }
    }
}
