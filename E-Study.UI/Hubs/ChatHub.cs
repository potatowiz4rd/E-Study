using Microsoft.AspNetCore.SignalR;

namespace E_Study.UI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        // You can add more methods as needed for your chat functionality
    }
}
