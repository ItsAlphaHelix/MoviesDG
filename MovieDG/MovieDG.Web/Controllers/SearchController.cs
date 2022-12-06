namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Movies;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        public async Task<IActionResult> MoviesByTitle(string title)
        {
            var movies = await this.searchService.SearchMovieByTitleAsync(title);

            if (!movies.Any())
            {
                return this.RedirectToAction(nameof(this.NoResults));
            }

            this.ViewData["CurrentSearchResult"] = title;

            var viewModel = new SearchMovieViewModel()
            {
                Movies = movies
            };

            return View(viewModel);
        }

        public IActionResult NoResults()
        {
            return this.View();
        }
    }
}
