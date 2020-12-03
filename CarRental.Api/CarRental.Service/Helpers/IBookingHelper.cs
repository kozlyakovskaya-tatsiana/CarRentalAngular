using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.Helpers
{
    public interface IBookingHelper
    {
        IEnumerable<string> GetBookingStatusNames();
    }
}
