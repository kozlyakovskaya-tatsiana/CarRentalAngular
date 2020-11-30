using System;

namespace CarRental.DAL.Enums
{
    public static class BookingStatusExtension
    {
        public static string GetStatusName(this BookingStatus bookingStatus)
        {
            switch (bookingStatus)
            {
                case BookingStatus.Open:
                    return BookingStatusValue.Open;
                case BookingStatus.Approved:
                    return BookingStatusValue.Approved;
                case BookingStatus.RejectedByManager:
                    return BookingStatusValue.RejectedByManager;
                case BookingStatus.RejectedByUser:
                    return BookingStatusValue.RejectedByUser;
                case BookingStatus.Closed:
                    return BookingStatusValue.Closed;
                default:
                    throw new Exception($"There is no name for {bookingStatus} status");
            }
        }
    }
}
