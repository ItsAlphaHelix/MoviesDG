﻿namespace MovieDG.Web.Controllers
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

        public async Task<IActionResult> Year(string year, int pageNumber, int pageSize = 10)
        {
            var totalPages = await this.moviesService.GetMoviesTotalPagesAsync(year, null, null, null);
            ViewBag.TotalPages = totalPages;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var movies = await this.moviesService.GetMoviesByYear(year, pageNumber, pageSize);

                //if (pageSize > movies.Count())
                //{
                //    return View();
                //}

                return PartialView("_LoadMoreMovies", movies);
            }
            else
            {
                var movies = await this.moviesService.GetMoviesByYear(year, 1, pageSize);

                return View(movies);
            }
        }
    }
}
