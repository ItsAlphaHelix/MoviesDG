namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    public class ActorsController : Controller
    {
        private readonly IMovieService movieService;
        public ActorsController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        public async Task<IActionResult> Name(string name)
        {
            var movies = await this.movieService.GetMoviesByActorAsync(name);

            return View(movies);
        }
    }
}
