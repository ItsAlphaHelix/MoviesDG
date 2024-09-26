namespace MovieDG.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Common;

    [Area("Administration")]
    [Authorize(Roles = "Admin, Suporter, Moderator")]
    public class DashboardController : Controller
    {
        [HttpGet("/Administration")]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
