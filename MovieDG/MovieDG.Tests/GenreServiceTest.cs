namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    public class GenreServiceTest
    {
        private IGenreService genreService;
        private EfRepository<Genre> genreRepository;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDataBase();
            genreService = new GenreService(genreRepository);
        }

        [Test]
        public async Task GetAllGenresAsyncTest()
        {
            await SeedDB.SeedGenres(genreRepository);

            var countries = await this.genreService.GetAllGenresAsync();

            Assert.That(countries.Count(), Is.EqualTo(10));
        }
        private void SetupInMemoryDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            genreRepository = new EfRepository<Genre>(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
