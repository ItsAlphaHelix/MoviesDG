namespace MovieDG.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using MovieDG.Core.ViewModels.Messages;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    public class ChatHub : Hub
    {
        private readonly IRepository<Message> messageRepository;

        public ChatHub(IRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task SendMessage(string text)
        {
            string name = Context?.User?.Identity?.Name ?? "";

            var message = new Message()
            {
                Name = name,
                Text = text
            };

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
    }
}
