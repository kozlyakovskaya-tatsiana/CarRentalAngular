using System;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;

namespace CarRental.DAL.Repositories
{
    public interface IBookingRepository : IRepository<BookingInfo>
    {
        Task BookCarAsync(BookingInfo booking);

        Task ApproveBookingAsync(Guid bookingId);

        Task RejectBookingAsync(Guid bookingId, BookingStatus rejectingStatus);

        Task CloseBookingAsync(Guid bookingId);
    }
}
