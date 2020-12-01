using CarRental.Service.WebModels.Booking;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

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

    }
}
