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
        public async Task<IActionResult> All(int pageNumber, int pageSize = 10)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var movies = await this.movieService.GetAllMoviesAsync(pageNumber, pageSize);

                if (pageNumber > movies.Count())
                {
                    return View();
                }

                return PartialView("_LoadMoreMovies", movies);
            }
            else
            {
                var movies = await this.movieService.GetAllMoviesAsync(1, pageSize);
                return View(movies);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> Filter(string filterType)
        {

            switch (filterType)
            {
                case "recent":

                    var recentMovies = await this.movieService.GetRecentMoviesAsync();
                    ViewData["Title"] = "Recent Movies";

                    return PartialView("_MoviesPartial", recentMovies);
                case "top-rated":

                    var topRatedMovies = await this.movieService.GetTopRatedMoviesAsync();

                    ViewData["Title"] = "Top Rated Movies";
                    return PartialView("_MoviesPartial", topRatedMovies);
                default:

                    var popularityMovies = await this.movieService.GetPopularityMoviesAsync();
                    ViewData["Title"] = "Popularity Movies";

                    return PartialView("_MoviesPartial", popularityMovies);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> TopRatedMovies()
        {
            var movies = await this.movieService.GetTopRatedMoviesAsync();
           
            return View(movies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PopularityMovies()
        {
            var movies = await this.movieService.GetPopularityMoviesAsync();

            return View(movies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RecentMovies()
        {
            var movies = await this.movieService.GetRecentMoviesAsync();

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
