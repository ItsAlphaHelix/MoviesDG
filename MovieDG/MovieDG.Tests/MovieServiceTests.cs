namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using NUnit.Framework;

    [TestFixture]
    public class MovieServiceTests
    {
        private EfRepository<Movie> movieRepository;
        private IMovieService movieService;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDatabase();
            movieService = new MovieService(movieRepository, null);
        }

        [Test]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(2)] //This is well test.
        public async Task GetMovieDetailsByIDTest(int id)
        {
            await SeedData(movieRepository);

            var movie = await movieService.GetMovieDetailsAsync(id);

            Assert.Multiple(() =>
            {
                Assert.That(movie, Is.Not.Null);
                Assert.That(movie.Id, Is.EqualTo(id));
            });
        }


        [Test]
        public async Task GetTop10RatedMoviesTest()
        {
            await SeedData(movieRepository);

            var movies = await movieService.GetTopRatedMoviesAsync();
            var expectedResult = movieRepository.AllAsNoTracking().OrderByDescending(x => x.AverageVotes).Take(10);

            Assert.That(movies.Count(), Is.EqualTo(expectedResult.Count()));
        }

        [Test]

        public async Task Get10PopularityMoviesTest()
        {
            await SeedData(movieRepository);

            var movies = await movieService.GetPopularityMoviesAsync();

            Assert.That(movies.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task Get10RecentMoviesTest()
        {
            await SeedData(movieRepository);

            var movies = await movieService.GetRecentMoviesAsync();

            Assert.That(movies.Count(), Is.EqualTo(10));

        }

        [Test]
        public async Task GetLatestMovieTest()
        {
            await SeedData(movieRepository);

            var movie = await movieService.GetLatestMovieAsync();

            Assert.That(movie, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(movie.ReleaseDate.Year, Is.EqualTo(2022));
                Assert.That(movie.ReleaseDate.Month, Is.EqualTo(12));
                Assert.That(movie.ReleaseDate.Day, Is.EqualTo(29));
            });
        }

        private void SetupInMemoryDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            movieRepository = new EfRepository<Movie>(dbContext);

        }

        private static async Task SeedData(EfRepository<Movie> movieRepository)
        {
            await movieRepository.AddAsync(new Movie()
            {
                Id = 1000,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 10,
                Popularity = 100001,
                ReleaseDate = new DateTime(2022, 12, 29)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 5,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 9,
                Popularity = 100000,
                ReleaseDate = new DateTime(2022, 12, 28)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 20,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 8,
                Popularity = 90000,
                ReleaseDate = new DateTime(2022, 12, 27)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 2,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 7,
                Popularity = 80000,
                ReleaseDate = new DateTime(2022, 12, 26)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 15,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 5,
                Popularity = 70000,
                ReleaseDate = new DateTime(2022, 12, 25)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 90,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 4,
                Popularity = 60000,
                ReleaseDate = new DateTime(2022, 12, 24)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 100,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 3,
                Popularity = 50000,
                ReleaseDate = new DateTime(2022, 12, 23)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 3,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 2,
                Popularity = 40000,
                ReleaseDate = new DateTime(2022, 12, 22)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 19,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 2.9,
                Popularity = 30000,
                ReleaseDate = new DateTime(2022, 12, 21)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 25,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 1,
                Popularity = 20000,
                ReleaseDate = new DateTime(2022, 12, 20)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 21,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 6,
                Popularity = 29999,
                ReleaseDate = new DateTime(2022, 12, 19)
            });
            await movieRepository.AddAsync(new Movie()
            {
                Id = 11,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 9.8,
                Popularity = 9999,
                ReleaseDate = new DateTime(2022, 12, 18)
            });

            await movieRepository.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
