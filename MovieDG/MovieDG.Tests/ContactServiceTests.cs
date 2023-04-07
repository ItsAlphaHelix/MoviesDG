namespace MovieDG.Tests
{
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

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDataBase();
            contactService = new ContactService(contactRepository, null);
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

            Assert.That(submisions.Result.Any());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetSubmisionByIdTest(int id)
        {
            await SeedData();

            await contactRepository.AddAsync(new Contact()
            {
                Id = 2,
                Name = "Petar",
                Email = "petar@gamilc.om",
                Subject = "Hello",
                Message = "Hello Hello???",
            });

            await contactRepository.SaveChangesAsync();

            var submision = await contactService.GetSubmisionByIdAsync(id);

            Assert.That(submision, Is.Not.Null);
            Assert.That(submision.Id, Is.EqualTo(id));
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

        [Test]
        public async Task DeleteQuestionTest()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            //using for call Dispose method explicitly;
            using var repo = new EfRepository<Contact>(dbContext);
            var service = new ContactService(repo, null);

            var contactQuestion = new Contact()
            {
                Name = "Ivan",
                Email = "ivan@gmail.com",
                Subject = "Test",
                Message = "How can i create good test?"
            };

            await dbContext.Contacts.AddAsync(contactQuestion);
            await dbContext.SaveChangesAsync();

            var contactQuestionForDelete = await dbContext.Contacts.FindAsync(contactQuestion.Id);

            dbContext.Entry(contactQuestionForDelete).State = EntityState.Detached;
            await service.DeleteQuestionAsync(contactQuestion.Id);

            var result = service.GetSubmisionsAsync();

            Assert.That(result.Result.Count(), Is.EqualTo(0));
        }

        private void SetupInMemoryDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            contactRepository = new EfRepository<Contact>(dbContext);
        }

        private async Task SeedData()
        {
            var contact  = new Contact
            {
                Id = 1,
                Name = "Dimityr",
                Email = "dimityr@gmail.com",
                Subject = "Testing",
                Message = "How to make good test?"
            };
           
            await this.contactRepository.AddAsync(contact);
            await this.contactRepository.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
