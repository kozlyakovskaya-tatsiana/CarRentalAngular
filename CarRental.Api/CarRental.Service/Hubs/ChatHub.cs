using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRental.Service.Hubs.Models;
using Microsoft.AspNetCore.SignalR;

namespace CarRental.Service.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(Message message)
        {
            await Clients.Others.SendAsync("MessageReceived", message);
        }
    }
}
