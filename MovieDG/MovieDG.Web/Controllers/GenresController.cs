namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;

    public class GenresController : Controller
    {
        private readonly IMovieService moviesService;

        public GenresController(IMovieService moviesService)
        {
            this.moviesService = moviesService;
        }
        public async Task<IActionResult> Type(string name, int pageNumber, int pageSize = 10)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var movies = await this.moviesService.GetMoviesByGenreAsync(name, pageNumber, pageSize);

                if (pageSize > movies.Count())
                {
                    return View();
                }

                return PartialView("_LoadMoreMovies", movies);
            }
            else
            {
                var movies = await this.moviesService.GetMoviesByGenreAsync(name, 1, pageSize);

                return View(movies);
            }
        }
    }
}
