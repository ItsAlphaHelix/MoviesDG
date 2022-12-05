namespace MovieDG.Web.Areas.Administration.Controllers
{
    using AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;

    public class ContactController : AdministrationController
    {
        private readonly IContactService contactsService;

        public ContactController(IContactService contactsService)
        {
            this.contactsService = contactsService;
        }
        public async Task<IActionResult> Submisions()
        {
            var questions = await this.contactsService.GetSubmisionsAsync();

            return View(questions);
        }

        public async Task<IActionResult> Details(int id)
        {
            var questions = await this.contactsService.GetSubmisionByIdAsync(id);

            return View(questions);
        }

        public IActionResult Reply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reply(ReplyMessageViewModel replyModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(replyModel);
            }

            await this.contactsService.ReplyMessageToUser(replyModel);

            return RedirectToAction(nameof(Reply));
        }

        public IActionResult Answer()
        {
            return View();
        }
    }
}
