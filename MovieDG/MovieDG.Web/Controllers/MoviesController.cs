namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using System.Security.Claims;

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> MineDetails(int id)
        {
            var movies = await this.movieService.GetMovieDetailsAsync(id);

            return View(movies);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var userId = GetUserID();

            await movieService.AddMovieToCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }
        public async Task<IActionResult> Mine()
        {
            var userId = GetUserID();

            var model = await movieService.GetAllMyMoviesAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = GetUserID();

            await movieService.RemoveMovieFromCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> RemoveAllFromCollection(int movieId)
        {
            var userId = GetUserID();

            await movieService.RemoveAllMoviesFromCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }

        private string GetUserID()
            => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
