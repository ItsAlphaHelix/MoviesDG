
namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Messages;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    public class ChatService : IChatService
    {
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public ChatService(
            IRepository<Message> messageRepository, IRepository<ApplicationUser> userRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<MessageViewModel>> GetAllMessagesAsync()
        {
            var messages = await this.messageRepository.AllAsNoTracking()
                .Select(x => new MessageViewModel()
            {
                FromName = x.Name,
                Text = x.Text
            }).ToListAsync();

            return messages;
        }
    }
}
