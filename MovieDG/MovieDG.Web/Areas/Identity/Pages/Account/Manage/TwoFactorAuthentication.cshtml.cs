namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using MovieDG.Data.Data.Models;
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
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
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
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.signInManager.ForgetTwoFactorClientAsync();
            this.toastNotification.Success("The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.");
            return this.RedirectToPage();
        }
    }
}
