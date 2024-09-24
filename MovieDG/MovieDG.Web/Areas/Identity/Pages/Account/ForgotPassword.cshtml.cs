namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Core.Messaging;
    using MovieDG.Common;
    using AspNetCoreHero.ToastNotification.Abstractions;

    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly INotyfService toastNotification;

        public ForgotPasswordModel(
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
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByEmailAsync(this.Input.Email);
                
                if (user == null || !(await this.userManager.IsEmailConfirmedAsync(user)))
                {
                    this.toastNotification.Error("Invalid email address.");
                    return this.Page();
                }

                this.Response.Cookies.Append("Username", user.UserName);

                var code = await this.userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: this.Request.Scheme);

                await this.emailSender.SendEmailAsync(
                    GlobalConstants.AppEmail,
                    GlobalConstants.SystemName,
                    this.Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                this.toastNotification.Information("Go to your email to change your password.");
                return this.Page();
            }

            return this.Page();
        }
    }
}
