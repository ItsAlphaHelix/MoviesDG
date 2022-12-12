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
        public async Task GetMovieDetailsTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var firstMovie = await movieService.GetMovieDetailsAsync(5);
            var secondMovie = await movieService.GetMovieDetailsAsync(20);
            var ThirdMovie = await movieService.GetMovieDetailsAsync(2);

            Assert.Multiple(() =>
            {
                Assert.That(firstMovie.Id, Is.EqualTo(5));
                Assert.That(secondMovie.Id, Is.EqualTo(20));
                Assert.That(ThirdMovie.Id, Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GetTop10RatedMoviesTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movies = await movieService.GetTopRatedMoviesAsync();


            Assert.Multiple(() =>
            {
                Assert.That(movies.Any(x => x.AverageVotes == 11));
                Assert.That(movies.Any(x => x.AverageVotes == 10));
                Assert.That(movies.Any(x => x.AverageVotes == 9));
                Assert.That(movies.Any(x => x.AverageVotes == 8));
                Assert.That(movies.Any(x => x.AverageVotes == 7));
                Assert.That(movies.Any(x => x.AverageVotes == 6));
                Assert.That(movies.Any(x => x.AverageVotes == 5));
                Assert.That(movies.Any(x => x.AverageVotes == 4));
                Assert.That(movies.Any(x => x.AverageVotes == 3));
                Assert.That(movies.Any(x => x.AverageVotes == 2.9));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Get10PopularityMoviesTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movies = await movieService.GetPopularityMoviesAsync();

            Assert.Multiple(() =>
            {
                Assert.That(movies.Any(x => x.Popularity == 100001));
                Assert.That(movies.Any(x => x.Popularity == 100000));
                Assert.That(movies.Any(x => x.Popularity == 90000));
                Assert.That(movies.Any(x => x.Popularity == 80000));
                Assert.That(movies.Any(x => x.Popularity == 70000));
                Assert.That(movies.Any(x => x.Popularity == 60000));
                Assert.That(movies.Any(x => x.Popularity == 50000));
                Assert.That(movies.Any(x => x.Popularity == 40000));
                Assert.That(movies.Any(x => x.Popularity == 30000));
                Assert.That(movies.Any(x => x.Popularity == 29999));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Get10RecentMoviesTest()
        {
            EfRepository<Movie> repository = Mocking();
            await SeedDb(repository);

            var movies = await movieService.GetRecentMoviesAsync();

            Assert.That(movies.Count(), Is.EqualTo(10));
            
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
                Popularity = 70000
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
                Popularity = 60000
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
                Popularity = 50000
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
                Popularity = 40000
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
                Popularity = 30000
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
                Popularity = 29999
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
                Popularity = 9999
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
