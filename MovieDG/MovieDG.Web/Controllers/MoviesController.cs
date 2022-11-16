namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Services.Contracts;
    using MoviesDG.Web.Extensions;

    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var movies = await this.movieService.GetlAllMoviesAsync();

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        { 
            var movies = await this.movieService.GetMovieDetailAsync(id);

            return View(movies);
        }
    }
}
