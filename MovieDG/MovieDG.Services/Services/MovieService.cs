namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Actors;
    using MovieDG.Core.ViewModels.Countries;
    using MovieDG.Core.ViewModels.Genres;
    using MovieDG.Core.ViewModels.Movies;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> moviesRepository;
        public MovieService(IRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync()
        {
            var movies = await moviesRepository
                .AllAsNoTracking()
                .Select(x => new MovieViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Poster = x.Poster,
                    Trailer = x.Trailer,
                    Popularity = x.Popularity,
                    AverageVotes = x.AverageVotes
                })
                .ToListAsync();

            return movies;
        }

        public async Task<MovieDetailsViewModel> GetMovieDetailsAsync(int id)
        {
            var movie = await moviesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MovieDetailsViewModel()
                {
                    Title = x.Title,
                    Poster = x.Poster,
                    Runtime = x.Runtime,
                    ReleaseDate = x.ReleaseDate,
                    Overview = x.Overview,
                    Trailer = x.Trailer,
                    Genres = x.MovieGenres.Select(x => new GenreViewModel()
                    {
                        Name = x.Genre.Type
                    }),
                    Actors = x.MovieActors.Select(x => new ActorViewModel()
                    {
                        Name = x.Actor.Name,
                        CharacterName = x.CharacterName
                    }),
                    Countries = x.MovieCountries.Select(x => new CountryViewModel()
                    {
                        Name = x.Country.Name
                    })
                })
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new NullReferenceException("The movie can not be null");
            }

            return movie;

        }

        public async Task<IEnumerable<MovieViewModel>> GetTopRatedMoviesAsync()
        {
            var topRatedMovies = await this.moviesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.AverageVotes)
                .Select(x => new MovieViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Poster = x.Poster,
                    Trailer = x.Trailer,
                    Popularity = x.Popularity,
                    AverageVotes = x.AverageVotes
                })
                .Take(10)
                .ToListAsync();

            return topRatedMovies;
        }

        public async Task<IEnumerable<MovieViewModel>> GetPopularityMoviesAsync()
        {
            var popularityMovies = await this.moviesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Popularity)
                .Select(x => new MovieViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Poster = x.Poster,
                    Trailer = x.Trailer,
                    Popularity = x.Popularity,
                    AverageVotes = x.AverageVotes
                })
                .Take(10)
                .ToListAsync();

            return popularityMovies;
        }

        public async Task<IEnumerable<MovieViewModel>> GetRecentMoviesAsync()
        {
            var recentMovies = await this.moviesRepository
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.ReleaseDate)
                 .Select(x => new MovieViewModel()
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Poster = x.Poster,
                     Trailer = x.Trailer,
                     Popularity = x.Popularity,
                     AverageVotes = x.AverageVotes,
                     Runtime = x.Runtime,
                     ReleaseDate = x.ReleaseDate
                 })
                 .Take(10)
                 .ToListAsync();

            return recentMovies;
        }

        public async Task<BannerMovieViewModel> GetLatestMovie()
        {
            var latestMovie = await this.moviesRepository
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.ReleaseDate)
                 .Select(x => new BannerMovieViewModel()
                 {
                     Title = x.Title,
                     Trailer = x.Trailer,
                     Banner = x.Banner,
                     Runtime = x.Runtime,
                     ReleaseDate = x.ReleaseDate,
                     Genres = x.MovieGenres.Select(x => new GenreViewModel()
                     {
                         Name = x.Genre.Type
                     }),
                 })
                 .FirstOrDefaultAsync();

            if (latestMovie == null)
            {
                throw new NullReferenceException("The movie can not be null");
            }

            return latestMovie;
        }
    }
}
