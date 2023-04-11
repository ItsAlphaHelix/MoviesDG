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


        [AllowAnonymous]
        public async Task<IActionResult> Filter(string filterType)
        {
            var movies = await this.movieService.GetAllMoviesAsync();
            string typeOfMovies = string.Empty;

            switch (filterType)
            {
                case "RecentMovies":
                    movies = await this.movieService.GetRecentMoviesAsync();
                    typeOfMovies = "Recent Movies";
                    break;
                case "TopRatedMovies":
                    movies = await this.movieService.GetTopRatedMoviesAsync();
                    typeOfMovies = "Top Rated Movies";
                    break;
                case "PopularityMovies":
                    movies = await this.movieService.GetPopularityMoviesAsync();
                    typeOfMovies = "Popularity Movies";
                    break;
            }

            ViewData["Title"] = typeOfMovies;
            return PartialView("_MoviesPartial", movies);
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
