using System;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;

namespace CarRental.DAL.Repositories.Realization
{
    public class BookingRepository : RepositoryBase<BookingInfo>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context) : base(context) { }

        public async Task BookCarAsync(BookingInfo booking)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    var carToBook = await Context.Cars.FindAsync(booking.CarId);
                    if(carToBook.Status != CarStatus.Free)
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
        }
    }
}
