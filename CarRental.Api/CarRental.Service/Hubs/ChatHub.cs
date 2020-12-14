using System.Threading.Tasks;
using CarRental.Service.Hubs.Models;
using CarRental.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarRental.Service.Hubs
{
    
    public class ChatHub : Hub
    {
        public async Task NewMessage(Message message)
        {
            await Clients.GroupExcept(message.Group, Context.ConnectionId).SendAsync("MessageReceived", message);
        }

        public async Task SendChatRequest()
        {
            await Clients.Groups("managers").SendAsync("ReceiveChatRequest", Context.ConnectionId);
        }

        [Authorize(Policy = Policy.ForManagersAdmins)]
        public async Task ApproveChatRequest(string fromConnectionId)
        {
            var groupName = Context.ConnectionId + fromConnectionId;

            await Groups.AddToGroupAsync(fromConnectionId, groupName);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.GroupExcept("managers", Context.ConnectionId).SendAsync("RequestApproved");

            await Clients.Group(groupName).SendAsync("StartChat", groupName);
        }

        [Authorize(Policy = Policy.ForManagersAdmins)]
        public async Task JoinToManagersGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "managers");
        }
    }
}
