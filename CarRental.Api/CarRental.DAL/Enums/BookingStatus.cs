using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Enums
{
    public enum BookingStatus
    {
        Open,
        Approved,
        RejectedByUser,
        RejectedByManager,
        Closed
    }
}
