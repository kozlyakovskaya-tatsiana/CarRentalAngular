using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.Service.WebModels.Booking;

namespace CarRental.Service.Services
{
    public interface IBookingService
    {
        Task BookCarAsync(BookingRequest bookingRequest);
    }
}
