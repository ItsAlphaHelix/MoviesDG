using Microsoft.AspNetCore.Mvc;
using MovieDG.Core.Contracts;
using MovieDG.Core.ViewModels.Contact;

namespace MovieDG.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactsService;

        public ContactController(IContactService contactsService)
        {
            this.contactsService = contactsService;
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

            await this.contactsService.GetUserCommentAsync(contactModel);

            //TODO: Make view for successfully subbmision 
            return Ok();
        }
    }
}
