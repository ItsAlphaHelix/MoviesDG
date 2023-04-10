﻿namespace MoviesDG.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Movies;
    using MovieDG.Web.Models;
    using System.Diagnostics;
    using System.Security.Claims;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService moviesService;
        private readonly IChatService chatService;
        public HomeController(
            ILogger<HomeController> logger,
            IMovieService moviesService,
            IChatService chatService
            )
        {
            _logger = logger;
            this.moviesService = moviesService;
            this.chatService = chatService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var latestMovie = await this.moviesService.GetLatestMovieAsync();
            var newMovies = await this.moviesService.GetRecentMoviesAsync();

            var viewModel = new HomepageViewModel()
            {
                LatestMovie = latestMovie,
                NewMovies = newMovies
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}