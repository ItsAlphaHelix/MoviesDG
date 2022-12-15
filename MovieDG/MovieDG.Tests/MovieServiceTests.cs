namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using NUnit.Framework;

    [TestFixture]
    public class MovieServiceTests
    {
        IRepository<ApplicationUser> userRepository;
        private IMovieService movieService;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                .UseInMemoryDatabase("MoviesDG")
                .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [Test]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(2)]
        public async Task GetMovieDetailsByIDTest(int id)
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movie = await movieService.GetMovieDetailsAsync(id);

            Assert.Multiple(() =>
            {
                Assert.That(movie, Is.Not.Null);

                Assert.That(movie.Id, Is.EqualTo(id));
            });
        }


        [Test]
        [TestCase(11)]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        [TestCase(6)]
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2.9)]
        public async Task GetTop10RatedMoviesTest(double averageVotes)
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movies = await movieService.GetTopRatedMoviesAsync();


            Assert.Multiple(() =>
            {
                Assert.That(movies.Any(x => x.AverageVotes == averageVotes));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        [TestCase(100001)]
        [TestCase(100000)]
        [TestCase(90000)]
        [TestCase(80000)]
        [TestCase(70000)]
        [TestCase(60000)]
        [TestCase(50000)]
        [TestCase(40000)]
        [TestCase(30000)]
        [TestCase(29999)]
        public async Task Get10PopularityMoviesTest(int popularity)
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movies = await movieService.GetPopularityMoviesAsync();

            Assert.Multiple(() =>
            {
                Assert.That(movies.Any(x => x.Popularity == popularity));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Get10RecentMoviesTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var expected = repository.AllAsNoTracking().OrderByDescending(x => x.ReleaseDate).Take(10).Count();
            var movies = await movieService.GetRecentMoviesAsync();
          
            Assert.That(movies.Count(), Is.EqualTo(expected));
            
        }

        [Test]
       public async Task GetLatestMovieTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movie = await movieService.GetLatestMovieAsync();

            Assert.Multiple(() =>
            {
                Assert.That(movie, Is.Not.Null);
                Assert.That(movie.ReleaseDate.Year, Is.EqualTo(2022));
                Assert.That(movie.ReleaseDate.Month, Is.EqualTo(12));
                Assert.That(movie.ReleaseDate.Day, Is.EqualTo(29));
            });
        }

        private EfRepository<Movie> Mocking()
        {
            var userMock = new Mock<IRepository<ApplicationUser>>();
            userRepository = userMock.Object;
            var repository = new EfRepository<Movie>(dbContext);
            movieService = new MovieService(repository, userRepository);
            return repository;
        }

        private static async Task SeedDb(EfRepository<Movie> repository)
        {
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
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
            await repository.AddAsync(new Movie()
            {
                Id = 11,
                Banner = "",
                IMDBLink = "",
                Overview = "",
                Poster = "",
                Title = "",
                Trailer = "",
                AverageVotes = 11,
                Popularity = 9999,
                ReleaseDate = new DateTime(2022, 12, 18)
            });

            await repository.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
