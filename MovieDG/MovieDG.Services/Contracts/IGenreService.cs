namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Genres;

    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();
    }
}
