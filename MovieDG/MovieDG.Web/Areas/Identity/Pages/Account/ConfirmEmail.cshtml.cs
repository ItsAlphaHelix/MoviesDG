namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using System.Text;
    using System.Threading.Tasks;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;

    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotyfService toastNotification;

        public ConfirmEmailModel(
            UserManager<ApplicationUser> userManager,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.toastNotification = toastNotification;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, userId));
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded == true)
            {
                this.toastNotification.Success(IdentityMessageConstants.SuccessfullyConfirmEmailMessage);

                return Page();
            }
            
            this.toastNotification.Error(IdentityErrorMessagesConstants.ConfirmEmailErrorMessage);

            return Page();
        }
    }
}
