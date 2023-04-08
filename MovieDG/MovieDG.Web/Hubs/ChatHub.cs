using Microsoft.AspNetCore.SignalR;

namespace MovieDG.Web.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId()
            => Context.ConnectionId;
    }
}
