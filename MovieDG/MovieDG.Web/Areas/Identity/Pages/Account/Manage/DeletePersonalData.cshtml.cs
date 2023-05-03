namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using MoviesDG.Data.Repositories;
    using System.ComponentModel.DataAnnotations;
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<DeletePersonalDataModel> logger;

        private readonly IRepository<UserMovie> userMoviesRepository;
        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IRepository<UserMovie> userMoviesRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.userMoviesRepository = userMoviesRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            var userMovies = this.userMoviesRepository.All().Where(x => x.UserId == user.Id);
            if (userMovies != null)
            {
                foreach (var userMovie in userMovies)
                {
                    this.userMoviesRepository.Delete(userMovie);
                }

                await this.userMoviesRepository.SaveChangesAsync();
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, this.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, IdentityErrorMessagesConstants.IncorrectPasswordErrorMessage);
                    return Page();
                }
            }

            var result = await this.userManager.DeleteAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(IdentityErrorMessagesConstants.DeleteUserErrorMessage);
            }

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation(String.Format(IdentityMessageConstants.DeleteUserLogMessage, userId));

            return this.Redirect("~/");
        }
    }
}
