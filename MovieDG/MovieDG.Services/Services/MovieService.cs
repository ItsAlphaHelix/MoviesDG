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
        private readonly IGenreService genresService;
        public MovieService(
            IRepository<Movie> moviesRepository,
            IGenreService genresService)
        {
            this.moviesRepository = moviesRepository;
            this.genresService = genresService;
        }

        public async Task<IEnumerable<MovieViewModel>> GetlAllMoviesAsync()
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
                    AverageVotes = x.AverageVotes,
                    TotalVotes = x.TotalVotes
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
    }
}
