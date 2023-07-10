namespace MovieDG.Tests
{
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ChatServiceTests
    {
        private EfRepository<Chat> chatRepository;
        private IChatService chatService;

        [SetUp]
        public void SetUp()
        {
            chatRepository = InMemoryDatabaseSetup.SetupWithoutUserRepo<Chat>();
            chatService = new ChatService(chatRepository);
        }

        [Test]
        public async Task  GetAllMessagesAsyncTest()
        {
            await SeedDB.SeedMessages(chatRepository);

            var messages = await chatService.GetAllMessagesAsync();

            Assert.That(messages.Count(), Is.EqualTo(10));
        }

        [TearDown]
        public void Dispose()
        {
            InMemoryDatabaseSetup.InMemoryDatabaseDispose();
        }
    }
}
