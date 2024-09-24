namespace MovieDG.Tests
{
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    public class GenreServiceTests
    {
        private IGenreService genreService;
        private EfRepository<Genre> genreRepository;

        [SetUp]
        public void SetUp()
        {
            genreRepository = InMemoryDatabaseSetup.SetupWithoutUserRepo<Genre>();
            genreService = new GenreService(genreRepository);
        }

        [Test]
        public async Task GetAllGenresAsyncTest()
        {
            await SeedDB.SeedGenres(genreRepository);

            var countries = await this.genreService.GetAllGenresAsync();

            Assert.That(countries.Count(), Is.EqualTo(10));
        }

        [TearDown]
        public void Dispose()
        {
            InMemoryDatabaseSetup.InMemoryDatabaseDispose();
        }
    }
}
