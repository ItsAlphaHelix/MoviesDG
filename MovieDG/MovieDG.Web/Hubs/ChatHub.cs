namespace MovieDG.Web.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    }
}
