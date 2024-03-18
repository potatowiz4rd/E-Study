using E_Study.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace E_Study.UI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync("receiveMessage", message);     
        

        // You can add more methods as needed for your chat functionality
    }
}
