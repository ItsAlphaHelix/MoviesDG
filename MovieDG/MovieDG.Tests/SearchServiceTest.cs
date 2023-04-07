namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    public class SearchServiceTest : MovieServiceTests
    {
        private ISearchService searchService;
        private EfRepository<Movie> movieRepository;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDataBase();
            searchService = new SearchService(movieRepository);
        }

        [Test]
        public async Task SearchMovieByTitleAsyncTest()
        {
            await SeedDB.SeedMovies(movieRepository);

            var movies = await this.searchService.SearchMovieByTitleAsync("Jhon Wick1");
            var movies2 = await this.searchService.SearchMovieByTitleAsync("Jhon");
            
            Assert.That(movies.Count(), Is.EqualTo(1));
            Assert.That(movies2.Count(), Is.EqualTo(10));
        }
        private void SetupInMemoryDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            movieRepository = new EfRepository<Movie>(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
