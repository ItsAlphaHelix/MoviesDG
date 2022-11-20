namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    public class CountriesController : Controller
    {
        private readonly IMovieService movieService;
        public CountriesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> Name(string name)
        {
            var movies = await this.movieService.GetMoviesByCountryAsync(name);

            return View(movies);
        }
    }
}
