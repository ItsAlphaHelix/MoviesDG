namespace MovieDG.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc; 
    public class ContactController : AdministrationController
    {
        public IActionResult Submisions()
        {
            return View();
        }
    }
}
