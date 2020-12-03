using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarRental.DAL.Enums;
using System.Linq;
using System.Reflection;

namespace CarRental.Service.Helpers.Realization
{
    public class BookingHelper : IBookingHelper
    {
        public IEnumerable<string> GetBookingStatusNames()
        {
            var statuses = Enum.GetValues(typeof(BookingStatus)).Cast<BookingStatus>().Select(st => st.GetType().GetMember(st.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name);

            return statuses;
        }
    }
}
