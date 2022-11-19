namespace MovieDG.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Movies;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;

    public class SearchService : ISearchService
    {
        private readonly IRepository<Movie> moviesRepository;

        public SearchService(IRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<MovieViewModel>> SearchMovieByTitleAsync(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var searchMovies = await this.moviesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                    .Select(x => new MovieViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Poster = x.Poster,
                        Trailer = x.Trailer,
                        Popularity = x.Popularity,
                        AverageVotes = x.AverageVotes
                    })
                    .ToListAsync();

                return searchMovies;
            }

            return null;
        }
    }
}
