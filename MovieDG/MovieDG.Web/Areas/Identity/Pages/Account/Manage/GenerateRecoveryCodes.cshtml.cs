#nullable disable

namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
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
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            var isTwoFactorEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user because they do not have 2FA enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            var isTwoFactorEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user as they do not have 2FA enabled.");
            }

            var recoveryCodes = await this.userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            RecoveryCodes = recoveryCodes.ToArray();

            this.logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
            this.toastNotification.Success("You have generated new recovery codes.");

            return RedirectToPage("./ShowRecoveryCodes");
        }
    }
}
