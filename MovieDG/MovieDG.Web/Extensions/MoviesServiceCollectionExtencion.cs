namespace MoviesDG.Web.Extensions
{
    using MovieDG.Services;
    using MovieDG.Services.Contracts;
    using MoviesDG.Data.Repositories;
    using MoviesDG.Services.DataApi;

    public static class MoviesServiceCollectionExtencion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICollectService, CollectService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IMovieService, MovieService>();

            return services;
        }
    }
}
