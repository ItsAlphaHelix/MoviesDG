#nullable disable

namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;

    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<GenerateRecoveryCodesModel> logger;
        private readonly INotyfService toastNotification;

        public GenerateRecoveryCodesModel(
            UserManager<ApplicationUser> userManager,
            ILogger<GenerateRecoveryCodesModel> logger,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.toastNotification = toastNotification;
        }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            var isTwoFactorEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException(IdentityErrorMessagesConstants.InvalidGenerateCodeErrorMessage);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            var isTwoFactorEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException(IdentityErrorMessagesConstants.InvalidGenerateCodeErrorMessage);
            }

            var recoveryCodes = await this.userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            RecoveryCodes = recoveryCodes.ToArray();

            this.logger.LogInformation(String.Format(IdentityMessageConstants.SuccessfullyGenerateCodeLogMessage, userId));
            this.toastNotification.Success(IdentityMessageConstants.SuccessfullyGenerateCodeMessage);

            return RedirectToPage("./ShowRecoveryCodes");
        }
    }
}
