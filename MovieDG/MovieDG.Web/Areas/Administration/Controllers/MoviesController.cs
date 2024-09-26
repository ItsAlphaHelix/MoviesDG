namespace MovieDG.Web.Areas.Administration.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Common;
    using MovieDG.Core.ViewModels.Movies;
    using MoviesDG.Core.DataApi;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using NUnit.Framework;
    using static MovieDG.Web.Areas.Administration.AdminMessageConstants.MessageConstants;

    [Area("Administration")]
    [Authorize(Roles = "Admin, Moderator")]
    public class MoviesController : Controller
    {
        private readonly ICollectService collectService;
        private readonly IRepository<Movie> moviesRepository;
        private readonly INotyfService toastNotification;

        public MoviesController(
            ICollectService collectService,
            IRepository<Movie> moviesRepository,
            INotyfService toastNotification)
        {
            this.collectService = collectService;
            this.moviesRepository = moviesRepository;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieInputViewModel inputModel)
        {
            if (inputModel.StartIndex > inputModel.EndIndex)
            {
                this.toastNotification.Error(MovieStartIndexErrorMessage);
                return this.View(inputModel);
            }

            for (int i = inputModel.StartIndex; i <= inputModel.EndIndex; i++)
            {
                var currentMovie = this.moviesRepository.AllAsNoTracking().FirstOrDefault(x => x.TMDBId == i);

                if (currentMovie is not null)
                {
                    this.toastNotification.Error(String.Format(MovieAlreadyExistMessage, i));

                    return RedirectToAction(nameof(Create));
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var movies = await this.collectService.AddMoviesToDatabaseAsync(inputModel.StartIndex, inputModel.EndIndex);

            this.toastNotification.Success(SuccessfullyAddMovieMessage);

            return RedirectToAction(nameof(Create));
        }
    }
}
