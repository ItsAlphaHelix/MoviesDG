namespace MovieDG.Core.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Core.Messaging;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> contactsRepository;
        private readonly IEmailSender emailSender;

        public ContactService(
            IRepository<Contact> contactsRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public async Task GetUserSubmisionsAsync(ContactInputViewModel model)
        {
            var submision = new Contact()
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };

            await this.contactsRepository.AddAsync(submision);
            await this.contactsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactViewModel>> GetSubmisionsAsync()
        {
            var submisions = await this.contactsRepository.
                AllAsNoTracking()
                .Select(x => new ContactViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Subject = x.Subject,
                    Message = x.Message
                })
                .ToListAsync();

            return submisions;
        }

        public async Task<ContactViewModel> GetSubmisionByIdAsync(int id)
        {
            var submision = await this.contactsRepository
                 .AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .Select(x => new ContactViewModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Email = x.Email,
                     Subject = x.Subject,
                     Message = x.Message
                 })
                 .FirstOrDefaultAsync();

            if (submision == null)
            {
                throw new NullReferenceException("The submision can not be null!");
            }

            return submision;
        }

        public async Task ReplyMessageToUserAsync(ReplyMessageViewModel replyModel)
        {
            await this.emailSender.SendEmailAsync
            (
                replyModel.AdminEmail,
                replyModel.Name,
                replyModel.ToUserEmail,
                replyModel.Subject,
                replyModel.Message
            );
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await this.contactsRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
            {
                throw new NullReferenceException("The question can not be null");
            }

            this.contactsRepository.Delete(question);

            await this.contactsRepository.SaveChangesAsync();
        }
    }
}
