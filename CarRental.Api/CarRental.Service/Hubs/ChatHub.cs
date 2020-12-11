using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Service.Hubs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarRental.Service.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task NewMessage(Message message)
        {
            await Clients.GroupExcept("managers", Context.ConnectionId).SendAsync("MessageReceived", message);
        }

        public async Task SendChatRequest()
        {
            await Clients.Groups("managers").SendAsync("ReceiveChatRequest", Context.ConnectionId);
        }

        public async Task ApproveChatRequest(string fromConnectionId)
        {
            var groupName = Context.ConnectionId + fromConnectionId;

            await Groups.AddToGroupAsync(fromConnectionId, groupName);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("StartChat", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task JoinToManagersGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "managers");
        }

    }
}
