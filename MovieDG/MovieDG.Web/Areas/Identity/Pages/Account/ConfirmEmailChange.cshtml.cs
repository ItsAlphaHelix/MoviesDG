

namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using System.Text;
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly INotyfService toastNotification;

        public ConfirmEmailChangeModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.toastNotification = toastNotification;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(IdentityErrorMessagesConstants.UserNullErrorMessage);
            }


            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                this.toastNotification.Error(IdentityErrorMessagesConstants.ChangeEmailErrorMessage);
                return RedirectToPage("./Manage/Email");
            }

            ////For now i dont want to change username
            //var setUserNameResult = await this.userManager.SetUserNameAsync(user, user.UserName);
            //if (!setUserNameResult.Succeeded)
            //{
            //    this.toastNotification.Error(IdentityErrorMessagesConstants.ChangeEmailErrorMessage);
            //    return RedirectToPage("./Manage/Email");
            //}

            await this.signInManager.RefreshSignInAsync(user);
            this.toastNotification.Success(IdentityMessageConstants.SuccessfullyChangedEmailMessage);
            return RedirectToPage("./Manage/Email");
        }
    }
}
