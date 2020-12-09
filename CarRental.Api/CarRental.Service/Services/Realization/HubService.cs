using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using CarRental.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CarRental.Service.Services.Realization
{
    public class HubService : IHubService
    {
        private readonly IHubContext<CarBookingHub> _bookingHub;

        public HubService(IHubContext<CarBookingHub> bookingHub)
        {
            _bookingHub = bookingHub;
        }
        public async Task ChangeCarStatus(Guid? carId, string newCarStatus)
        {
            await _bookingHub.Clients.All.SendAsync("ChangeCarStatus", carId, newCarStatus);
        }
    }
}
