using System.Collections.Generic;
using System.Linq;
using CarRental.Service.WebModels.Booking;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.BookingDtos;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Service.Services.Realization
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;

            _mapper = mapper;
        }

        public async Task BookCarAsync(BookingRequest bookingRequest)
        {
            var bookingInfo = _mapper.Map<BookingInfo>(bookingRequest);

            await _bookingRepository.BookCarAsync(bookingInfo);
        }

        public async Task<IEnumerable<BookingInfoForRead>> GetAllBookings()
        {
            var bookings = await (await _bookingRepository.GetAllAsync(includes:
                    b => b
                        .Include(booking => booking.Car.Documents)
                        .Include(booking => booking.Car.RentalPoint.Location.City.Country)))
                .ToArrayAsync();

            var bookingsForRead = _mapper.Map<BookingInfoForRead[]>(bookings);

            return bookingsForRead;
        }

    }
}
