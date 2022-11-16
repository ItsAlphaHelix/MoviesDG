namespace MovieDG.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Services.Contracts;
    using MovieDG.Services.Models.Movies;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> moviesRepository;
        public MovieService(IRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<MovieViewModel>> GetlAllMoviesAsync()
        {
            var movies = await this.moviesRepository
                .AllAsNoTracking()
                .Select(x => new MovieViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Poster = x.Poster,
                    ReleaseDate = x.ReleaseDate,
                    Runtime = x.Runtime,
                    Popularity = x.Popularity,
                    AverageVotes = x.AverageVotes,
                    TotalVotes = x.TotalVotes
                })
                .ToListAsync();

            return movies;
        }

        public async Task<MovieDetailsViewModel> GetMovieDetailAsync(int id)
        {
            var movie = await this.moviesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MovieDetailsViewModel()
                {
                    Banner = x.Banner,
                    Poster = x.Poster,
                    Runtime = x.Runtime,
                    ReleaseDate = x.ReleaseDate,
                    Overview = x.Overview,
                    Trailer = x.Trailer,
                }).FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new NullReferenceException("The movie can not be null");
            }

            return movie;

        }
    }
}
