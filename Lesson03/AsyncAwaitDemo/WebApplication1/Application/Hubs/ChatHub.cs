using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class ChatHub : Hub
    {
        //public Task OnConnected()
        //{

        //}
        public override async Task OnConnectedAsync()
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("newClientConnected", Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("clientDisconnected", Context.ConnectionId);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("newMessage", message);
        }
    }
}
