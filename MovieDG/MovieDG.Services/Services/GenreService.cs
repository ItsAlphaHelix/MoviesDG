namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Genres;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;

    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genresRepository;

        public GenreService(IRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }
        public IEnumerable<GenreViewModel> GetAllGenresAsync()
        {
            var genres = this.genresRepository
                .AllAsNoTracking()
                .Select(x => new GenreViewModel()
                {
                    Id = x.Id,
                    Name = x.Type
                })
                .ToList();

            return genres;
        }
    }
}
