using MovieDG.Core.ViewModels;
using MovieDG.Core.ViewModels.Messages;
using MovieDG.Data.Data.Models;

namespace MovieDG.Core.Contracts
{
    public interface IChatService
    {
        Task<IEnumerable<MessageViewModel>> GetAllMessagesAsync();
    }
}
