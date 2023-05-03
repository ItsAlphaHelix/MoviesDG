namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using System.Threading.Tasks;
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> logger;
        private readonly INotyfService toastNotification;

        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<TwoFactorAuthenticationModel> logger,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.toastNotification = toastNotification;
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        [BindProperty]
        public bool Is2faEnabled { get; set; }

        public bool IsMachineRemembered { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            this.HasAuthenticator = await this.userManager.GetAuthenticatorKeyAsync(user) != null;
            this.Is2faEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            this.IsMachineRemembered = await this.signInManager.IsTwoFactorClientRememberedAsync(user);
            this.RecoveryCodesLeft = await this.userManager.CountRecoveryCodesAsync(user);

            return this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            await this.signInManager.ForgetTwoFactorClientAsync();
            this.toastNotification.Success(IdentityMessageConstants.ForgottenBrowserMessage);
            return this.RedirectToPage();
        }
    }
}
