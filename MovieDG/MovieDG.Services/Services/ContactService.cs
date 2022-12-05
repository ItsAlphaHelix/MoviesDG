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
    using System.Security.Claims;
    using static MovieDG.Common.GlobalConstants;
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
                throw new NullReferenceException("Submision can not be null!");
            }

            return submision;
        }

        public async Task ReplyMessageToUser(ReplyMessageViewModel replyModel)
        {
            
        }
    }
}
