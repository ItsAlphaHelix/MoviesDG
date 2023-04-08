using MovieDG.Core.ViewModels;
using MovieDG.Data.Data.Models;

namespace MovieDG.Core.Contracts
{
    public interface IChatService
    {
        Task<Message>CreateMessageAsync(string userId, string message);

        Task<IEnumerable<MessageViewModel>> GetAllMessagesAsync();
    }
}
