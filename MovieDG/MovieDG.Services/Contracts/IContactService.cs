using MovieDG.Core.ViewModels.Contact;
using MovieDG.Data.Data.Models;

namespace MovieDG.Core.Contracts
{
    public interface IContactService
    {
        Task GetUserSubmisionsAsync(ContactInputViewModel model);

        Task<IEnumerable<ContactViewModel>> GetSubmisionsAsync();

        Task<ContactViewModel> GetSubmisionByIdAsync(int id);

        Task ReplyMessageToUserAsync(ReplyMessageViewModel replyModel);

        Task DeleteQuestionAsync(int id);
    }
}
