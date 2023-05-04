namespace MovieDG.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using MovieDG.Core.ViewModels.Messages;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    public class ChatHub : Hub
    {
        private readonly IRepository<Chat> messageRepository;
        private static int connectedClientsCount = 0;
        public ChatHub(
            IRepository<Chat> messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public async Task SendMessage(string text)
        {
            string name = Context?.User?.Identity?.Name ?? "";

            var message = new Chat()
            {
                Name = name,
                Text = text
            };

            if (string.IsNullOrWhiteSpace(message.Text) || message.Text.Contains('<'))
            {
                throw new ArgumentException("Invalid message!");
            }

            var messageModel = new MessageViewModel()
            {
                FromName = message.Name,
                Text = message.Text
            };

            await messageRepository.AddAsync(message);
            await messageRepository.SaveChangesAsync();

            //message send to all users
            await Clients.All.SendAsync("ReceiveMessage", messageModel);
        }

        public override async Task OnConnectedAsync()
        {
            connectedClientsCount++;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            connectedClientsCount--;
            await base.OnDisconnectedAsync(exception);
        }

        public static bool AreClientsDisconnected()
        {
            return connectedClientsCount == 0;
        }
    }
}
