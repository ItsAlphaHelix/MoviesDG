using Microsoft.AspNetCore.Mvc;

namespace MovieDG.Web.Areas.Administration.Controllers
{
    public class MoviesController : AdministrationController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
