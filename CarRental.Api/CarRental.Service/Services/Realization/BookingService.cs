using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Service.WebModels.Booking;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using CarRental.DAL.Repositories;
using CarRental.Service.DTO.BookingDtos;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Service.Services.Realization
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly ICarRepository _carRepository;

        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, ICarRepository carRepository)
        {
            _bookingRepository = bookingRepository;

            _mapper = mapper;

            _carRepository = carRepository;
        }

        public async Task BookCarAsync(BookingRequest bookingRequest)
        {
            var bookingInfo = _mapper.Map<BookingInfo>(bookingRequest);

            Car car = null;

            if (bookingInfo.CarId != null)
            {
                car = await _carRepository.FindByIdAsync((Guid)bookingInfo.CarId);
            }

            bookingInfo.Car = car;

            bookingInfo.Sum = ((bookingInfo.EndDateOfRenting - bookingInfo.StartDateOfRenting).Days + 1) * bookingInfo.Car.CostPerDay;

            await _bookingRepository.BookCarAsync(bookingInfo);
        }

        public async Task ApproveBookingAsync(Guid bookingId)
        {
            await _bookingRepository.ApproveBookingAsync(bookingId);
        }

        public async Task<IEnumerable<BookingInfoForRead>> GetAllBookingsAsync()
        {
            var bookings = await (await _bookingRepository.GetAllAsync(includes:
                    b => b
                        .Include(booking => booking.Car.Documents)
                        .Include(booking => booking.Car.RentalPoint.Location.City.Country)))
                .ToArrayAsync();

            var bookingsForRead = _mapper.Map<BookingInfoForRead[]>(bookings);

            return bookingsForRead;
        }

        public async Task<IEnumerable<BookingInfoForRead>> GetBookingsByStatusAsync(BookingStatus bookingStatus)
        {
            var bookings = await (await _bookingRepository.GetAllAsync(b => b.BookingStatus == bookingStatus,
                    b => b
                        .Include(booking => booking.Car.Documents)
                        .Include(booking => booking.Car.RentalPoint.Location.City.Country)))
                .ToArrayAsync();

            var bookingsForRead = _mapper.Map<BookingInfoForRead[]>(bookings);

            return bookingsForRead;
        }

        public async Task<IEnumerable<BookingInfoForRead>> GetUserBookingsByStatusAsync(string userId, BookingStatus bookingStatus)
        {
            var bookings = await (await _bookingRepository.GetAllAsync(
                    b => b.BookingStatus == bookingStatus && b.User.Id == userId,
                    b => b
                        .Include(booking => booking.Car.Documents)
                        .Include(booking => booking.Car.RentalPoint.Location.City.Country)))
                .ToArrayAsync();

            var bookingsForRead = _mapper.Map<BookingInfoForRead[]>(bookings);

            return bookingsForRead;
        }

        public async Task RejectBookingByManagerAsync(Guid bookingId)
        {
            await _bookingRepository.RejectBookingAsync(bookingId, BookingStatus.RejectedByManager);
        }

        public async Task RejectBookingByUserAsync(Guid bookingId)
        {
            await _bookingRepository.RejectBookingAsync(bookingId, BookingStatus.RejectedByUser);
        }

        public async Task<IEnumerable<BookingInfoForRead>> GetAllUserBookings(string userId)
        {
            var bookings = await (await _bookingRepository.GetAllAsync(includes:
                    b => b
                        .Include(booking => booking.Car.Documents)
                        .Include(booking => booking.Car.RentalPoint.Location.City.Country)))
                .Where(b => b.UserId == userId)
                .ToArrayAsync();

            var bookingsForRead = _mapper.Map<BookingInfoForRead[]>(bookings);

            return bookingsForRead;
        }

        public async Task CloseBookingAsync(Guid bookingId)
        {
            await _bookingRepository.CloseBookingAsync(bookingId);
        }
    }
}
