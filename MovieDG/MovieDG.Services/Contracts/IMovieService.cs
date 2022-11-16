using MovieDG.Services.Models.Movies;

namespace MovieDG.Services.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetlAllMoviesAsync();

        Task<MovieDetailsViewModel> GetMovieDetailAsync(int id);
    }
}
