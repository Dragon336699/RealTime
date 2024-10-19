using Microsoft.AspNetCore.SignalR;

namespace RealTimeChat.Server
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceivePrivateMessage", message);   
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await Clients.All.SendAsync("ReceiveMessage", $"{userId} has joined");
        }
    }
}
