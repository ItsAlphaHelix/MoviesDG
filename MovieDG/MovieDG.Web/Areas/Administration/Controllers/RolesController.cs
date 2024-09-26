namespace MovieDG.Web.Areas.Administration.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Common;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Users;
    using MovieDG.Web.Areas.Administration.AdminMessageConstants;

    [Area("Administration")]
	public class RolesController : Controller
    {
        private readonly IRoleService roleService;
        private readonly INotyfService toastNotification;
        public RolesController(
            IRoleService roleService,
            INotyfService toastNotification)
        {
            this.roleService = roleService;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
		[Authorize(Roles = "Admin, Moderator, Suport")]
		public async Task<IActionResult> AllUsersInRoles()
        {
            var users = await this.roleService.GetAllUsersRolesAsync();

            return View(users);
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RemoveUserFromRole(string username, string role)
        {
            string loginUsername = this.User.Identity.Name;
            await this.roleService.RemoveRoleFromUser(username, role, loginUsername);

            this.toastNotification.Success(String.Format(MessageConstants.SuccessfullyDeletedUserFromRole, role));
            return RedirectToAction(nameof(AllUsersInRoles));
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Add(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Add));
            }

            try
            {
                await this.roleService.AddUserToRoleAsync(user.UserName, user.Role);
                toastNotification.Success(String.Format(MessageConstants.SuccessfullyUserIsAddedToRole, user.Role));
            }
            catch (Exception ex)
            {
                toastNotification.Error(ex.Message);
            }

            return RedirectToAction(nameof(Add));
        }
    }
}
