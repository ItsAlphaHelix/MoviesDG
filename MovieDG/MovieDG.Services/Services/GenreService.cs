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
        public async Task<IEnumerable<GenreViewModel>> GetAllGenresAsync()
        {
            var genres = await this.genresRepository
                .AllAsNoTracking()
                .Select(x => new GenreViewModel()
                {
                    Id = x.Id,
                    Name = x.Type
                })
                .ToListAsync();

            return genres;
        }
    }
}
