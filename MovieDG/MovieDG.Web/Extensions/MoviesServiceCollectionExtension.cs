namespace MoviesDG.Web.Extensions
{
    using AspNetCoreHero.ToastNotification;
    using Microsoft.AspNetCore.Identity;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Hubs.ChatHubServices;
    using MoviesDG.Core.DataApi;
    using MoviesDG.Core.Messaging;
    using MoviesDG.Data.Repositories;

    public static class MoviesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICollectService, CollectService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IRoleService, RoleService>();

            string smptServer = configuration["BrevoSmtpSettings:Server"];
            string port = configuration["BrevoSmtpSettings:Port"];
            string username = configuration["BrevoSmtpSettings:Username"];
            string password = configuration["BrevoSmtpSettings:Password"];

            services.AddScoped<IEmailSender>(x => new EmailSender(smptServer, int.Parse(port), username, password));

            services.AddSignalR();
            services.AddHostedService<ChatResetService>();

            services.AddNotyf(configuration =>
            {
                configuration.DurationInSeconds = 5;
                configuration.IsDismissable = true;
                configuration.Position = NotyfPosition.TopRight;
            });

            return services;
        }
    }
}
