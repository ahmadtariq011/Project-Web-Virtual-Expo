using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Web.Hubs
{
    public class ChatHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;
        public ChatHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public override Task OnConnectedAsync()
        {
            var context = _contextAccessor.HttpContext;
            //var ExhibitionName = ExhibitionHub.ExhibitionName;
            string ExhibitionName = context.Session.GetString("ExhibitionName");
            Groups.AddToGroupAsync(Context.ConnectionId, ExhibitionName);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            BllMessage bllMessage = new BllMessage();
            Message messageDB = new Message();
            messageDB.ExhibitionIdentifier = receiver;
            messageDB.MessageText = message;
            messageDB.SenderName = sender;
            bllMessage.Insert(messageDB);
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
