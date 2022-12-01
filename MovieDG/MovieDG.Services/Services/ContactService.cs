namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;
    using MoviesDG.Core.Messaging;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;

    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> contactsRepository;
        private readonly IEmailSender emailSender;

        public ContactService(
            IRepository<Contact> contactsRepository,
            IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public async Task GetUserCommentAsync(ContactInputViewModel model)
        {
            var comment = new Contact()
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };

            await this.contactsRepository.AddAsync(comment);
            await this.contactsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactViewModel>> GetCommentsAsync()
        {
            var comments = await this.contactsRepository.
                AllAsNoTracking()
                .Select(x => new ContactViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Subject,
                    Message = x.Message
                })
                .ToListAsync();

            return comments;
        }
    }
}
