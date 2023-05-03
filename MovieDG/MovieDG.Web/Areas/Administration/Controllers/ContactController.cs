namespace MovieDG.Web.Areas.Administration.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Common;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;
    using MovieDG.Data.Data.Models;
    using static MovieDG.Web.Areas.Administration.AdminConstants.AdminMessageConstants;

    public class ContactController : AdministrationController
    {
        private readonly IContactService contactService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotyfService toastNotification;
        public ContactController(
            IContactService contactService,
            UserManager<ApplicationUser> userManager,
            INotyfService toastNotification)
        {
            this.contactService = contactService;
            this.userManager = userManager;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Submisions()
        {
            var questions = await this.contactService.GetSubmisionsAsync();

            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var questions = await this.contactService.GetSubmisionByIdAsync(id);

            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Reply(int id)
        {
            var question = await this.contactService.GetSubmisionByIdAsync(id);
            var user = await this.userManager.GetUserAsync(this.User);

            var replyModel = new ReplyMessageViewModel()
            {
                Id = question.Id,
                Name = $"From: {GlobalConstants.SystemName} - {user.UserName}",
                AdminEmail = user.Email,
                ToUserEmail = question.Email,
                Subject = $"Subject: {question.Subject}"
            };

            return View(replyModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(ReplyMessageViewModel replyModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(replyModel);
            }

            await this.contactService.ReplyMessageToUserAsync(replyModel);

            this.toastNotification.Success(SuccessSentAnswer);

            return RedirectToAction(nameof(Reply));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.contactService.DeleteQuestionAsync(id);

            this.toastNotification.Success(SuccessDeleteQuestion);

            return this.RedirectToAction(nameof(this.Submisions));
        }
    }
}
