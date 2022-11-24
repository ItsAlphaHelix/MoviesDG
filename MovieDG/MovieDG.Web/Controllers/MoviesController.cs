namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Movies;
    using System.Security.Claims;

    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var movies = await this.movieService.GetAllMoviesAsync();

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movies = await this.movieService.GetMovieDetailsAsync(id);

            return View(movies);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await movieService.AddMovieToCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await movieService.GetAllMyMoviesAsync(userId);

            return View(model);
        }
    }
}
