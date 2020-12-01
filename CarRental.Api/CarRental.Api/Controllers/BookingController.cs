using System.Threading.Tasks;
using CarRental.Api.Security;
using CarRental.Service.Services;
using CarRental.Service.WebModels.Booking;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var bookings = await _bookingService.GetAllBookings();

            return Ok(bookings);
        }
    }
}
