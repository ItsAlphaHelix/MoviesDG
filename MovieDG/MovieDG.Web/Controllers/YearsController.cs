namespace MovieDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    public class YearsController : Controller
    {
        private readonly IMovieService moviesService;

        public YearsController(IMovieService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> Year(string year)
        {
            var movies = await this.moviesService.GetMoviesByYear(year);

            return View(movies);
        }
    }
}
