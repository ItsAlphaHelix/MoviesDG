namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Messages;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> messageRepository;

        public ChatService(
            IRepository<Chat> messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public async Task<IEnumerable<MessageViewModel>> GetAllMessagesAsync()
        {
            var messages = await this.messageRepository.All()
                .Select(x => new MessageViewModel()
            {
                FromName = x.Name,
                Text = x.Text
            }).ToListAsync();

            return messages;
        }
    }
}
