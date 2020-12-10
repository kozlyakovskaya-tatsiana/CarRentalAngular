using System;
using System.Threading.Tasks;
using CarRental.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CarRental.Service.Services.Realization
{
    public class HubService : IHubService
    {
        private readonly IHubContext<CarBookingHub> _bookingHub;

        private readonly IHubContext<ChatHub> _chatHub;

        public HubService(IHubContext<CarBookingHub> bookingHub, IHubContext<ChatHub> chatHub)
        {
            _bookingHub = bookingHub;

            _chatHub = chatHub;
        }

        public async Task ChangeCarStatus(Guid? carId, string newCarStatus)
        {
            await _bookingHub.Clients.All.SendAsync("ChangeCarStatus", carId, newCarStatus);
        }
    }
}
