namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Movies;
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync();

        Task<MovieDetailsViewModel> GetMovieDetailsAsync(int id);

        Task<IEnumerable<MovieViewModel>> GetTopRatedMoviesAsync();

        Task<IEnumerable<MovieViewModel>> GetPopularityMoviesAsync();

        Task<IEnumerable<MovieViewModel>> GetRecentMoviesAsync();

        Task<BannerHomeMovieViewModel> GetLatestMovie();
    }
}
