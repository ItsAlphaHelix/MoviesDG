using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MovieDG.Core.Contracts;
using MovieDG.Web.Hubs;
using MoviesDG.Web.Controllers;
using System.Security.Claims;

namespace MovieDG.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(
            string message,
            [FromServices] IHubContext<ChatHub> chat)
        {

            var userId = this.GetUserID();

            var Message = await this.chatService.CreateMessageAsync(userId, message);

            await chat.Clients.User(userId).SendAsync("RecieveMessage", userId, message);

            //await chat.Clients.Group(message)
            //    .SendAsync("RecieveMessage", new
            //    {
            //        Text = Message.Text,
            //        Name = Message.Name
            //    });

            return Ok();
        }

        private string GetUserID()
            => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
