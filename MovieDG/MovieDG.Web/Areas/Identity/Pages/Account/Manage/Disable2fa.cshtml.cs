namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;

    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<Disable2faModel> logger;
        private readonly INotyfService toastNotification;

        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.toastNotification = toastNotification;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            if (!await this.userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException(String.Format(IdentityErrorMessagesConstants.InvalidDisable2FAErrorMessage, user.Id));
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            var disable2faResult = await this.userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException(String.Format(IdentityErrorMessagesConstants.Unexpected2FAErrorMessage, user.Id));
            }

            this.logger.LogInformation(String.Format(IdentityMessageConstants.SuccessfullyDisabled2FALogMessage, user.Id));
            this.toastNotification.Success(IdentityMessageConstants.SuccessfullyDisabled2FAMessage);
            return this.RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
