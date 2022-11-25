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

        Task<BannerHomeMovieViewModel> GetLatestMovieAsync();

        Task<IEnumerable<MovieViewModel>> GetMoviesByGenreAsync(string name);

        Task<IEnumerable<MovieViewModel>> GetMoviesByCountryAsync(string name);

        Task<IEnumerable<MovieViewModel>> GetMoviesByActorAsync(string name);

        Task AddMovieToCollectionAsync(int movieId, string userId);

        Task<IEnumerable<MovieViewModel>> GetAllMyMoviesAsync(string userId);

        Task RemoveMovieFromCollectionAsync(int movieId, string userId);

        Task RemoveAllMoviesFromCollectionAsync(int movieId, string userId);
    }
}
