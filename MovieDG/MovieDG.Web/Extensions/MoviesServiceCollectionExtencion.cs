namespace MoviesDG.Web.Extensions
{
    using AspNetCoreHero.ToastNotification;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MoviesDG.Core.DataApi;
    using MoviesDG.Core.Messaging;
    using MoviesDG.Data.Repositories;

    public static class MoviesServiceCollectionExtencion
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
            services.AddScoped<IEmailSender>(x => new SendGridEmailSender(configuration.GetSection("SendGridKey").Value));

            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddNotyFService(this IServiceCollection services)
        {
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
