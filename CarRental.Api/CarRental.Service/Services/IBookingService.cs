using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.DAL.Enums;
using CarRental.Service.DTO.BookingDtos;
using CarRental.Service.WebModels.Booking;

namespace CarRental.Service.Services
{
    public interface IBookingService
    {
        Task BookCarAsync(BookingRequest bookingRequest);

        Task ApproveBookingAsync(Guid bookingId);

        Task<IEnumerable<BookingInfoForRead>> GetAllBookingsAsync();

        Task<IEnumerable<BookingInfoForRead>> GetBookingsByStatusAsync(BookingStatus bookingStatus);

        Task RejectBookingByManagerAsync(Guid bookingId);

        Task RejectBookingByUserAsync(Guid bookingId);

        Task<IEnumerable<BookingInfoForRead>> GetAllUserBookings(string userId);

        Task<IEnumerable<BookingInfoForRead>> GetUserBookingsByStatusAsync(string userId, BookingStatus bookingStatus);

        Task CloseBookingAsync(Guid bookingId);
    }
}
