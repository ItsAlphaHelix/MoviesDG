namespace MovieDG.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;

    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService contactsService;
        private readonly INotyfService toastNotification;
        public ContactController(
            IContactService contactsService,
            INotyfService toastNotification)
        {
            this.contactsService = contactsService;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactInputViewModel contactModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(contactModel);
            }

            await this.contactsService.GetUserSubmisionsAsync(contactModel);

            this.toastNotification.Success("You successfully send message! Please wait for response.");
            return RedirectToAction(nameof(ContactUs));
        }
    }
}
