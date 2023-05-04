namespace MovieDG.Web.Hubs.ChatHubServices
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Hubs;
    using MoviesDG.Data.Repositories;
    public class ChatResetService : BackgroundService
    {
        private readonly IHubContext<ChatHub> hubContext;
        private readonly IServiceScopeFactory scopeFactory;

        public ChatResetService(IHubContext<ChatHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            this.hubContext = hubContext;
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

                if (ChatHub.AreClientsDisconnected())
                {
                    using var scope = scopeFactory.CreateScope();
                    var messageRepository = scope.ServiceProvider.GetRequiredService<IRepository<Chat>>();
                    var messages = await messageRepository.AllAsNoTracking().ToListAsync();

                    foreach (var message in messages)
                    {
                        messageRepository.Delete(message);
                    }

                    await messageRepository.SaveChangesAsync();

                    // Notify connected clients that the chat has been reset
                    await hubContext.Clients.All.SendAsync("ChatReset");
                }
            }
        }
    }
}
