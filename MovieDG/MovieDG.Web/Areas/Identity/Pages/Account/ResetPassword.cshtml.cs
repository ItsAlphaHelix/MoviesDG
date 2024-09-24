
namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotyfService toastNotification;
        public ResetPasswordModel(
            UserManager<ApplicationUser> userManager,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.toastNotification = toastNotification;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = IdentityErrorMessagesConstants.PasswordErrorMessage, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = IdentityErrorMessagesConstants.ConfirmPasswordErrorMessage)]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return this.BadRequest(IdentityErrorMessagesConstants.PasswordCodeNullErrorMessage);
            }
            else
            {
                this.Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),
                };
                return this.Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            string username = this.Request?.Cookies["Username"];

            var user = await this.userManager.FindByNameAsync(username);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToPage("./Login");
            }

            var result = await this.userManager.ResetPasswordAsync(user, this.Input.Code, this.Input.Password);
            
            if (result.Succeeded)
            {
                this.toastNotification.Success("Successfully reset password.");
                this.Response.Cookies.Delete("Username");
                return this.RedirectToPage("./Login");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }
    }
}
