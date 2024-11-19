namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using System.Security.Claims;

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService moviesService;

        public MoviesController(IMovieService movieService)
        {
            this.moviesService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, null, null, null);
            ViewBag.TotalPages = totalPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var movies = await this.moviesService.GetAllMoviesAsync(pageNumber, pageSize);

                return PartialView("_LoadMoreMovies", movies);
            }
            else
            {
                var movies = await this.moviesService.GetAllMoviesAsync(1, pageSize);
                return View(movies);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> Filter(string filterType)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, null, null, filterType);
            ViewBag.TotalPages = totalPages;

            switch (filterType)
            {
                case "recent":

                    var recentMovies = await this.moviesService.GetRecentMoviesAsync(1, 10);
                    ViewData["Title"] = "Recent Movies";

                    return PartialView("_MoviesPartial", recentMovies);
                case "top-rated":

                    var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync(1, 10);

                    ViewData["Title"] = "Top Rated Movies";
                    return PartialView("_MoviesPartial", topRatedMovies);
                default:

                    var popularityMovies = await this.moviesService.GetPopularityMoviesAsync(1, 10);
                    ViewData["Title"] = "Popularity Movies";

                    return PartialView("_MoviesPartial", popularityMovies);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoadMore(string filterType, int pageNumber, int pageSize = 10)
        {

            switch (filterType)
            {
                case "recent":
                    var recentMovies = await this.moviesService.GetRecentMoviesAsync(pageNumber, pageSize);

                    ViewData["Title"] = "Recent Movies";

                    return PartialView("_LoadMoreMovies", recentMovies);
                case "top-rated":

                    var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync(pageNumber, pageSize);

                    ViewData["Title"] = "Top Rated Movies";

                    return PartialView("_LoadMoreMovies", topRatedMovies);
                case "popularity":

                    var popularityMovies = await this.moviesService.GetPopularityMoviesAsync(pageNumber, pageSize);

                    ViewData["Title"] = "Popularity Movies";

                    return PartialView("_LoadMoreMovies", popularityMovies);
                  
            }

            var movies = await this.moviesService.GetAllMoviesAsync(pageNumber, pageSize);
            return PartialView("_LoadMoreMovies", movies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> TopRatedMovies(int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, null, null, "top-rated");
            ViewBag.TotalPages = totalPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync(pageNumber, pageSize);

                return PartialView("_LoadMoreMovies", topRatedMovies);
            }
            else
            {
                var movies = await this.moviesService.GetTopRatedMoviesAsync(1, pageSize);
                return View(movies);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> PopularityMovies(int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, null, null, "popularity");
            ViewBag.TotalPages = totalPages;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var popularityMovies = await this.moviesService.GetPopularityMoviesAsync(pageNumber, pageSize);

                return PartialView("_LoadMoreMovies", popularityMovies);
            }
            else
            {
                var popularityMovies = await this.moviesService.GetPopularityMoviesAsync(1, pageSize);
                return View(popularityMovies);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> RecentMovies(int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(null, null, null, "recent");
            ViewBag.TotalPages = totalPages;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var recentMovies = await this.moviesService.GetRecentMoviesAsync(pageNumber, pageSize);

                return PartialView("_LoadMoreMovies", recentMovies);
            }
            else
            {
                var recentMovies = await this.moviesService.GetRecentMoviesAsync(1, pageSize);
                return View(recentMovies);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var movies = await this.moviesService.GetMovieDetailsAsync(id);

            return View(movies);
        }

        public async Task<IActionResult> MineDetails(int id)
        {
            var movies = await this.moviesService.GetMovieDetailsAsync(id);

            return View(movies);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var userId = GetUserID();

            await moviesService.AddMovieToCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }
        public async Task<IActionResult> Mine()
        {
            var userId = GetUserID();

            var model = await moviesService.GetAllMyMoviesAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = GetUserID();

            await moviesService.RemoveMovieFromCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> RemoveAllFromCollection(int movieId)
        {
            var userId = GetUserID();

            await moviesService.RemoveAllMoviesFromCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }
        private string GetUserID()
            => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
