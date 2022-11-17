namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Movies;
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetlAllMoviesAsync();

        Task<MovieDetailsViewModel> GetMovieDetailsAsync(int id);
    }
}
