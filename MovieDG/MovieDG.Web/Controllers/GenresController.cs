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
        public async Task<IActionResult> Type(string name)
        {
            var movies = await this.moviesService.GetMoviesByGenreAsync(name);

            return View(movies);
        }
    }
}
