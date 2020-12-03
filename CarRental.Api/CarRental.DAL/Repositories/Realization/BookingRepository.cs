using System;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarRental.DAL.Repositories.Realization
{
    public class BookingRepository : RepositoryBase<BookingInfo>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context) : base(context) { }

        public async Task BookCarAsync(BookingInfo booking)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var carToBook = await Context.Cars.FindAsync(booking.CarId);
                if (carToBook.Status != CarStatus.Free)
                    throw new Exception($"Car with id={carToBook.Id} isn't free. Unable to book it.");

                carToBook.Status = CarStatus.BookingRequest;

                booking.Car = carToBook;
                booking.BookingStatus = BookingStatus.Open;

                await Context.Bookings.AddAsync(booking);
                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task ApproveBookingAsync(Guid bookingId)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var booking =  Context.Bookings.Include(b => b.Car).FirstOrDefault(b => b.Id == bookingId);
                if (booking == null)
                    throw new Exception($"Booking with id={bookingId} isn't found.");

                booking.BookingStatus = BookingStatus.Approved;

                booking.Car.Status = CarStatus.InRent;

                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task RejectBookingAsync(Guid bookingId, BookingStatus rejectingStatus)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var booking = Context.Bookings.Include(b => b.Car).FirstOrDefault(b => b.Id == bookingId);
                if (booking == null)
                    throw new Exception($"Booking with id={bookingId} isn't found.");

                booking.BookingStatus = rejectingStatus;

                booking.Car.Status = CarStatus.Free;

                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task CloseBookingAsync(Guid bookingId)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var booking = Context.Bookings.Include(b => b.Car).FirstOrDefault(b => b.Id == bookingId);
                if (booking == null)
                    throw new Exception($"Booking with id={bookingId} isn't found.");

                booking.BookingStatus = BookingStatus.Closed;

                booking.Car.Status = CarStatus.Free;

                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
