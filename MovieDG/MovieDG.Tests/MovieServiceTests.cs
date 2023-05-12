namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using NUnit.Framework;

    [TestFixture]
    public class MovieServiceTests
    {
        private EfRepository<Movie> movieRepository;
        private EfRepository<ApplicationUser> userRepository;
        private IMovieService movieService;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDatabase();
            movieService = new MovieService(movieRepository, userRepository);
        }

        [Test]
        public async Task GetlAllMoviesTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetAllMoviesAsync(1, 10);
            Assert.That(movies.Count, Is.EqualTo(10));

        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(5)]
        public async Task GetMovieDetailsAsyncTest(int id)
        {
            await SeedDB.SeedMovies(this.movieRepository);
            var movie = await this.movieService.GetMovieDetailsAsync(id);

            Assert.That(movie.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(2000)]
        [TestCase(-1)]
        public async Task GetMovieDetailsAsyncShouldThrowsArgumentException(int id)
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => this.movieService.GetMovieDetailsAsync(id));

            Assert.That(ex.Message, Is.EqualTo($"The movie can not be null"));
        }

        [Test]
        public async Task GetTop10RatedMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetTopRatedMoviesAsync(1, 10);
            double maxAverageVote = movies.FirstOrDefault().AverageVotes;

            Assert.Multiple(() =>
            {
                Assert.That(maxAverageVote, Is.EqualTo(11.5));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Get10PopularityMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetPopularityMoviesAsync(1, 10);
            double popularMovie = movies.FirstOrDefault()?.Popularity ?? 0;

            Assert.Multiple(() =>
            {
                Assert.That(popularMovie, Is.EqualTo(10));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Get10RecentMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetRecentMoviesAsync(1, 10);
            double recentMovie = movies.FirstOrDefault()?.Popularity ?? 0;
            Assert.Multiple(() =>
            {
                Assert.That(recentMovie, Is.EqualTo(10));
                Assert.That(movies.Count(), Is.EqualTo(10));
            });
        }

        [Test]
        public async Task GetMoviesByGenreAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetMoviesByGenreAsync("Fantasy1", 1 , 10);

            Assert.That(movies.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GetMoviesByCountryAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetMoviesByCountryAsync("Bulgaria1");

            Assert.That(movies.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetMoviesByActorNameAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetMoviesByActorAsync("Actor1");

            Assert.That(movies.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task AddMovieToUserCollectionTest()
        {
            await SeedDB.SeedUsers(userRepository);
            await SeedDB.SeedMovies(movieRepository);

            var movie = await movieRepository.AllAsNoTracking().ToListAsync();
            var user = await userRepository.AllAsNoTracking().ToListAsync();

            int movieId = movie.FirstOrDefault().Id;
            string userId = user.FirstOrDefault().Id;

            await movieService.AddMovieToCollectionAsync(movieId, userId);

            var afterAdding = await movieRepository.AllAsNoTracking().Include(x => x.UsersMovies).FirstOrDefaultAsync();

            Assert.Multiple(() =>
            {
                Assert.That(movie, Is.Not.Null);
                Assert.That(user, Is.Not.Null);
                Assert.That(afterAdding.UsersMovies.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task GetAllUserMoviesTest()
        {
            await SeedDB.SeedUsers(userRepository);
            await SeedDB.SeedMovies(movieRepository);

            var movie = await movieRepository.AllAsNoTracking().ToListAsync();
            var user = await userRepository.AllAsNoTracking().ToListAsync();

            int movieID = movie.FirstOrDefault().Id;
            string userID = user.FirstOrDefault().Id;

            await movieService.AddMovieToCollectionAsync(movieID, userID);

            var movies = movieService.GetAllMyMoviesAsync(userID);

            Assert.That(movies.Result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveMovieFromCollectionTest()
        {
            await SeedDB.SeedUsers(userRepository);
            await SeedDB.SeedMovies(movieRepository);

            var movie = await movieRepository.AllAsNoTracking().ToListAsync();
            var user = await userRepository.AllAsNoTracking().ToListAsync();

            int movieID = movie.FirstOrDefault().Id;
            string userID = user.FirstOrDefault().Id;

            await movieService.AddMovieToCollectionAsync(movieID, userID);

            var movieForRemove = movieService.RemoveMovieFromCollectionAsync(movieID, userID).ToString();

            Assert.That(movieForRemove.Any());
        }

        [Test]
        public async Task RemoveAllMovieFromUserCollection()
        {
            await SeedDB.SeedUsers(userRepository);
            await SeedDB.SeedMovies(movieRepository);

            var movie = await movieRepository.AllAsNoTracking().ToListAsync();
            var user = await userRepository.AllAsNoTracking().ToListAsync();

            int movieID = movie.FirstOrDefault().Id;
            string userID = user.FirstOrDefault().Id;

            var movies = movieService.RemoveAllMoviesFromCollectionAsync(movieID, userID).ToString();

            Assert.That(movies.Any());
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
            userRepository = new EfRepository<ApplicationUser>(dbContext);

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}