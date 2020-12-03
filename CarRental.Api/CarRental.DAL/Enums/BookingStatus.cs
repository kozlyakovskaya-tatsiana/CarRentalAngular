using System.ComponentModel.DataAnnotations;

namespace CarRental.DAL.Enums
{
    public enum BookingStatus
    {
        [Display(Name = BookingStatusValue.Open)]
        Open,

        [Display(Name = BookingStatusValue.Approved)]
        Approved,

        [Display(Name = BookingStatusValue.RejectedByUser)]
        RejectedByUser,

        [Display(Name = BookingStatusValue.RejectedByManager)]
        RejectedByManager,

        [Display(Name = BookingStatusValue.Closed)]
        Closed
    }
}
