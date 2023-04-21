namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Genres;
    using MovieDG.Core.ViewModels.Movies;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Models;

    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync(int pageNumber, int pageSize);

        Task<MovieDetailsViewModel> GetMovieDetailsAsync(int id);

        Task<IEnumerable<MovieViewModel>> GetTopRatedMoviesAsync(int pageNumber, int pageSize);

        Task<IEnumerable<MovieViewModel>> GetPopularityMoviesAsync(int pageNumber, int pageSize);

        Task<IEnumerable<MovieViewModel>> GetRecentMoviesAsync(int pageNumber, int pageSize);

        Task<BannerHomeMovieViewModel> GetMovieForHomepage();

        Task<IEnumerable<MovieViewModel>> GetMoviesByGenreAsync(string name);

        Task<IEnumerable<MovieViewModel>> GetMoviesByCountryAsync(string name);

        Task<IEnumerable<MovieViewModel>> GetMoviesByActorAsync(string name);

        Task AddMovieToCollectionAsync(int movieId, string userId);

        Task<IEnumerable<MovieViewModel>> GetAllMyMoviesAsync(string userId);

        Task RemoveMovieFromCollectionAsync(int movieId, string userId);

        Task RemoveAllMoviesFromCollectionAsync(int movieId, string userId);
        Task<IEnumerable<MovieViewModel>> GetAllYearsAsync();

        Task<IEnumerable<MovieViewModel>> GetMoviesByYear(string year);

        Task<IEnumerable<MovieViewModel>> GetRecentCarouselMovies();
    }
}
