using MovieDG.Core.ViewModels.Contact;

namespace MovieDG.Core.Contracts
{
    public interface IContactService
    {
        Task GetUserCommentAsync(ContactInputViewModel model);

        Task<IEnumerable<ContactViewModel>> GetCommentsAsync();
    }
}
