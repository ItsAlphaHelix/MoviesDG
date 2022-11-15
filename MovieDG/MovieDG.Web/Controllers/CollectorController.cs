namespace MoviesDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using MoviesDG.Services.DataApi;
    using MoviesDG.Web.Models;

    public class CollectorController : Controller
    {
        private readonly ICollectService collectService;
        private readonly IRepository<Movie> moviesRepository;

        public CollectorController(
            ICollectService collectService,
            IRepository<Movie> moviesRepository)
        {
            this.collectService = collectService;
            this.moviesRepository = moviesRepository;
        }

        public IActionResult CollectData()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CollectData(GetDataInputModel inputModel)
        {
            // It could be done in a better way :/
            if (inputModel.StartIndex > inputModel.EndIndex)
            {
                this.ModelState.AddModelError(string.Empty, "End index cannot be less than Start index !");
                return this.View(inputModel);
            }

            for (int i = inputModel.StartIndex; i <= inputModel.EndIndex; i++)
            {
                var currentMovie = this.moviesRepository.AllAsNoTracking().FirstOrDefault(x => x.TMDBId == i);

                if (currentMovie is not null)
                {
                    this.ModelState.AddModelError(string.Empty, $"Movie with Id: ({i}) already exists !");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var movies = await this.collectService.AddMoviesToDatabaseAsync(inputModel.StartIndex, inputModel.EndIndex);

            return this.RedirectToAction(nameof(this.Success), new { count = movies });
        }

        public IActionResult Success(int count)
        {
            this.ViewData["count"] = count;
            return this.View(count);
        }
    }
}
