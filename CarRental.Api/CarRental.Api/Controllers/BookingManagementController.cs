using System.Threading.Tasks;
using CarRental.Api.Security;
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

        public BookingManagementController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var bookings = await _bookingService.GetAllBookings();

            return Ok(bookings);
        }
    }
}
