namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    public class CountriesController : Controller
    {
        private readonly IMovieService moviesService;
        public CountriesController(IMovieService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> Name(string name, int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, name, null, null);
            ViewBag.TotalPages = totalPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var movies = await this.moviesService.GetMoviesByCountryAsync(name, pageNumber, pageSize);

                return PartialView("_LoadMoreMovies", movies);
            }
            else
            {
                var movies = await this.moviesService.GetMoviesByCountryAsync(name, 1, pageSize);

                return View(movies);
            }
        }
    }
}
