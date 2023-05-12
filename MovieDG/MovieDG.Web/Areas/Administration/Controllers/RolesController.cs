namespace MovieDG.Web.Areas.Administration.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Users;
    using MovieDG.Web.Areas.Administration.AdminMessageConstants;
    public class RolesController : AdministrationController
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllUsersInRoles()
        {
            var users = await this.roleService.GetAllUsersRolesAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(string username, string role)
        {
            await this.roleService.RemoveRoleFromUser(username, role);

            this.toastNotification.Success(String.Format(AdminMessageConstants.SuccessfullyDeletedUserFromRole, role));
            return RedirectToAction(nameof(AllUsersInRoles));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Add));
            }

            try
            {
                await this.roleService.AddUserToRoleAsync(user.UserName, user.Role);
                toastNotification.Success(String.Format(AdminMessageConstants.SuccessfullyUserIsAddedToRole, user.Role));
            }
            catch (Exception ex)
            {
                toastNotification.Error(ex.Message);
            }

            return RedirectToAction(nameof(Add));
        }
    }
}
