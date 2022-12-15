namespace MovieDG.Tests
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Core.ViewModels.Contact;
    using MoviesDG.Core.Messaging;
    using MoviesDG.Data;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Repositories;
    using System.Linq;
    using System.Threading.Tasks;
    public class ContactServiceTests
    {
        private IContactService contactService;
        private EfRepository<Contact> contactRepository;
        private MovieDGDbContext dbContext;
        private Contact contact;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDataBase();
            this.contactService = new ContactService(this.contactRepository, null);
        }

        private void SetupInMemoryDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            this.contactRepository = new EfRepository<Contact>(dbContext);
        }

        [Test]
        public async Task GetContactSubmisionsTest()
        {

            var contact = new ContactInputViewModel()
            {
                Name = "Dimityr",
                Email = "dimityr@gmail.com",
                Subject = "Testing",
                Message = "Your test is very good!"
            };

            await contactService.GetUserSubmisionAsync(contact);

            var submisions = contactService.GetSubmisionsAsync();

            Assert.That(submisions.Result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllSubmisionsTest()
        {
            await SeedData();

            var submisions = contactService.GetSubmisionsAsync();

            Assert.That(submisions.Result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetSubmisionByIdTest()
        {
            await SeedData();
            var submision = await contactService.GetSubmisionByIdAsync(1);

            Assert.IsNotNull(submision);
            Assert.That(submision.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task ReplyMessageToUserTest()
        {
            var emailMock = new Mock<IEmailSender>();
            var service = new ContactService(contactRepository, emailMock.Object);

            var answer = new ReplyMessageViewModel
            {
                Name = "Dimityr",
                AdminEmail = "dimityr@gmail.com",
                ToUserEmail = "randomUser@gmail.com",
                Subject = "Testing",
                Message = "Do you like my tests?",
            };

            await service.ReplyMessageToUserAsync(answer);
        }

        //[Test]
        //public async Task DeleteQuestionTest()
        //{
        //    var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
        //                      .UseInMemoryDatabase("MoviesDG")
        //                      .Options;

        //    dbContext = new MovieDGDbContext(contextOptions);

        //    dbContext.Database.EnsureDeleted();
        //    dbContext.Database.EnsureCreated();

        //    using var repo = new EfRepository<Contact>(dbContext);
        //    var service = new ContactService(repo, null);

        //    var contactQuestion = new Contact()
        //    {
        //        Id = 2,
        //        Name = "Ivan",
        //        Email = "Ivan@gmail.com",
        //        Subject = "Test",
        //        Message = "How can i create good test?"
        //    };

        //    await dbContext.Contacts.AddAsync(contactQuestion);
        //    await contactRepository.SaveChangesAsync();

        //    var sub = await dbContext.Contacts.FindAsync(contactQuestion.Id);
        //    await service.DeleteQuestionAsync(sub.Id);

        //    var result = service.GetSubmisionByIdAsync(sub.Id);

        //    Assert.AreEqual(null, result);
        //}
        private async Task SeedData()
        {
            var contact1  = new Contact
            {
                Id = 1,
                Name = "Dimityr",
                Email = "dimityr@gmail.com",
                Subject = "Testing",
                Message = "How to make good test?"
            };

            await this.contactRepository.AddAsync(contact1);

            await this.contactRepository.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
