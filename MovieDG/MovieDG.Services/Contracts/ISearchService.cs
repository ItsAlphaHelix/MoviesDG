namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Movies;
    public interface ISearchService
    {
        Task<IEnumerable<MovieViewModel>> SearchMovieByTitleAsync(string title);
    }
}
