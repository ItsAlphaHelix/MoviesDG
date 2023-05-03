namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LoginWith2faModel> logger;

        public LoginWith2faModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginWith2faModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = IdentityErrorMessagesConstants.TwoFactorCodeErrorMessage, MinimumLength = 6)]
            [DataType(DataType.Text)]
            public string TwoFactorCode { get; set; }
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string? returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException(IdentityErrorMessagesConstants.UnableToLoadTwoFactorAuthUserErrorMessage);
            }

            this.ReturnUrl = returnUrl;
            this.RememberMe = rememberMe;

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe, string? returnUrl = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException(IdentityErrorMessagesConstants.UnableToLoadTwoFactorAuthUserErrorMessage);
            }

            var authenticatorCode = this.Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await this.signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, this.Input.RememberMachine);

            if (result.Succeeded)
            {
                this.logger.LogInformation(String.Format(IdentityMessageConstants.LoginWithTwoFactorAuthMessage, user.Id));
                return this.LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                this.logger.LogWarning(String.Format(IdentityMessageConstants.LockedoutAccountMessage, user.Id));
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                this.logger.LogWarning(IdentityMessageConstants.InvalidAuthCodeMessage, user.Id);
                this.ModelState.AddModelError(string.Empty, IdentityErrorMessagesConstants.InvalidAuthCodeErrorMessage);
                return this.Page();
            }
        }
    }
}
