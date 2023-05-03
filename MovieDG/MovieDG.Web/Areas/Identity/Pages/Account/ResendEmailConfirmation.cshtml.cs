namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Common;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using MoviesDG.Core.Messaging;

    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly INotyfService toastNotification;

        public ResendEmailConfirmationModel(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.toastNotification = toastNotification;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await this.userManager.FindByEmailAsync(this.Input.Email);
            if (user == null)
            {
                this.toastNotification.Success(IdentityMessageConstants.VerificationEmailSuccessfullySentMessage);
                return this.Page();
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: this.Request.Scheme);
            await this.emailSender.SendEmailAsync(
                GlobalConstants.AppEmail,
                GlobalConstants.SystemName,
                this.Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.toastNotification.Success(IdentityMessageConstants.VerificationEmailSuccessfullySentMessage);
            return this.Page();
        }
    }
}
