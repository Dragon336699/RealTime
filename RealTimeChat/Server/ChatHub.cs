using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeChat.Server
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceivePrivateMessage", message);   
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.UserIdentifier;
            await Clients.All.SendAsync("ReceiveMessage", $"{connectionId} has joined");
        }
    }
}
