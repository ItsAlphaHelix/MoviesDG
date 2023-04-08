using Microsoft.EntityFrameworkCore;
using MovieDG.Core.Contracts;
using MovieDG.Core.ViewModels;
using MovieDG.Data.Data.Models;
using MoviesDG.Data.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace MovieDG.Core.Services
{
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

        public async Task<Message> CreateMessageAsync(string userId, string message)
        {

            var user = await this.userRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

            var Message = new Message()
            {
                Text = message,
                Name = user.UserName
            };
            await messageRepository.AddAsync(Message);

            await messageRepository.SaveChangesAsync();

            return Message;
        }

        public async Task<IEnumerable<MessageViewModel>> GetAllMessagesAsync()
        {
            var messages = await this.messageRepository.AllAsNoTracking()
                .Select(x => new MessageViewModel()
            {
                Name = x.Name,
                Text = x.Text
            }).ToListAsync();

            return messages;
        }
    }
}
