namespace MovieDG.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Contact;
    using MovieDG.Common;

    [Authorize]
    public class ContactController : Controller
    {
        private const string EmailErrorMessage = "You can not use admin's email";
        private const string SuccessSentMessage = "Your message has been sent. Be patient you will receive a reply in your email within 1 day.";

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

            if (contactModel.Email == GlobalConstants.AppEmail)
            {
                this.ModelState.AddModelError("Email", EmailErrorMessage);
                return View();
            }

            await this.contactsService.GetUserSubmisionAsync(contactModel);

            this.toastNotification.Success(SuccessSentMessage);

            return RedirectToAction(nameof(ContactUs));
        }
    }
}
