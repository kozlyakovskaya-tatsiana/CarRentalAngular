using System;
using System.Threading.Tasks;
using CarRental.Api.Security;
using CarRental.DAL.Enums;
using CarRental.Service.Helpers;
using CarRental.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = Policy.ForManagersAdmins)]
    [Route("api/booking")]
    [ApiController]
    public class BookingManagementController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        private readonly IBookingHelper _bookingHelper;

        public BookingManagementController(IBookingService bookingService, IBookingHelper bookingHelper)
        {
            _bookingService = bookingService;

            _bookingHelper = bookingHelper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            return Ok(bookings);
        }

        [AllowAnonymous]
        [HttpGet("statuses")]
        public IActionResult StatusList()
        {
            var statuses = _bookingHelper.GetBookingStatusNames();

            return Ok(statuses);
        }

        [HttpGet("list/{status:int}")]
        public async Task<IActionResult> List(BookingStatus status)
        {
            var bookings = await _bookingService.GetBookingsByStatusAsync(status);

            return Ok(bookings);
        }

        [HttpGet("approve/{id}")]
        public async Task<IActionResult> ApproveBooking(Guid id)
        {
            await _bookingService.ApproveBookingAsync(id);

            return Ok();
        }

        [HttpGet("reject/{id}")]
        public async Task<IActionResult> RejectBooking(Guid id)
        {
            await _bookingService.RejectBookingByManagerAsync(id);

            return Ok();
        }

        [HttpGet("close/{id}")]
        public async Task<IActionResult> CloseBooking(Guid id)
        {
            await _bookingService.CloseBookingAsync(id);

            return Ok();
        }
    }
}
