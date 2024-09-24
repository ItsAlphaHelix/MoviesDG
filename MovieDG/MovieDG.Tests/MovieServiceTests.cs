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

        [SetUp]
        public void SetUp()
        {
            (movieRepository, userRepository) = InMemoryDatabaseSetup.SetupWithUserRepo<Movie>();
            movieService = new MovieService(movieRepository, userRepository);
        }

        [Test]
        public async Task GetAllMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetAllMoviesAsync(1, 10);
            Assert.That(movies.Count, Is.EqualTo(10));
        }

        [Test]
        [TestCase(47)]
        [TestCase(48)]
        [TestCase(60)]
        [TestCase(61)]
        public async Task GetMovieDetailsAsyncTest(int id)
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movie = await this.movieService.GetMovieDetailsAsync(id);
            Assert.That(movie.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(2000)]
        [TestCase(-1)]
        public async Task GetMovieDetailsAsyncShouldThrowsAnArgumentException(int id)
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => this.movieService.GetMovieDetailsAsync(id));
            Assert.That(ex.Message, Is.EqualTo($"The movie can not be null."));
        }

        [Test]
        public async Task GetTop10RatedMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetTopRatedMoviesAsync(1, 10);
            Assert.That(movies.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task Get10PopularityMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetPopularityMoviesAsync(1, 10);
            Assert.That(movies.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task Get10RecentMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetRecentMoviesAsync(1, 10);
            Assert.That(movies.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task GetMoviesByGenreAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetMoviesByGenreAsync("Fantasy1", 1, 10);

            Assert.That(movies.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GetMoviesByCountryAsyncTest()
        {
            await SeedDB.SeedMovies(this.movieRepository);

            var movies = await this.movieService.GetMoviesByCountryAsync("Bulgaria1", 1, 10);

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
        public async Task AddMovieToUserCollectionAsyncTest()
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
        public async Task GetAllUserMoviesAsyncTest()
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
        public async Task RemoveMovieFromCollectionAsyncTest()
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
        public async Task RemoveAllMovieFromUserCollectionAsyncTest()
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

        [Test]
        public async Task GetAllYearsAsyncTest()
        {
            await SeedDB.SeedMovies(movieRepository);

            var years = await this.movieService.GetAllYearsAsync();
            Assert.That(years.Count(), Is.EqualTo(30));
        }

        [Test]
        public async Task GetMoviesByYearAsyncTest()
        {
            await SeedDB.SeedMovies(movieRepository);

            var movie = await this.movieService.GetMoviesByYear("2023", 1, 10);
            var movieTitle = movie.FirstOrDefault().Title;

            Assert.That(movieTitle, Is.EqualTo("John Wick29"));
        }

        [Test]
        public async Task GetRecentCarouselMoviesAsyncTest()
        {
            await SeedDB.SeedMovies(movieRepository);

            var movies = await this.movieService.GetRecentCarouselMovies();

            Assert.That(movies.Count(), Is.EqualTo(20));
        }

        [TearDown]
        public void Dispose()
        {
            InMemoryDatabaseSetup.InMemoryDatabaseDispose();
        }
    }
}