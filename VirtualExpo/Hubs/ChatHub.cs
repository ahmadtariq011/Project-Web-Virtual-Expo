using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using VirtualExpo.Web.StaticClass;

namespace VirtualExpo.Web.Hubs
{
    public class ChatHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            var ExhibitionName = ExhibitionHub.ExhibitionName;
            Groups.AddToGroupAsync(Context.ConnectionId, ExhibitionName);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
