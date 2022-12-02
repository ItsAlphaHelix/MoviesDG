namespace MovieDG.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static MovieDG.Common.GlobalConstants;

    [Area("Administration")]
    [Authorize(Roles = AdminRoleName)]
    public class AdministrationController : Controller
    {
        
    }
}
