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

        [SetUp]
        public void SetUp()
        {
            movieRepository = InMemoryDatabaseSetup.SetupWithoutUserRepo<Movie>();
            searchService = new SearchService(movieRepository);
        }

        [Test]
        public async Task SearchMovieByTitleAsyncTest()
        {
            await SeedDB.SeedMovies(movieRepository);

            var movies = await this.searchService.SearchMovieByTitleAsync("John Wick1");
            var movies2 = await this.searchService.SearchMovieByTitleAsync("John");
            Assert.Multiple(() =>
            {
                Assert.That(movies.Count(), Is.EqualTo(11));
                Assert.That(movies2.Count(), Is.EqualTo(30));
            });
        }

        [TearDown]
        public void Dispose()
        {
            InMemoryDatabaseSetup.InMemoryDatabaseDispose();
        }
    }
}
