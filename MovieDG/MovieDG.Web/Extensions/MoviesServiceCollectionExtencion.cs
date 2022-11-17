namespace MoviesDG.Web.Extensions
{
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MoviesDG.Core.DataApi;
    using MoviesDG.Data.Repositories;

    public static class MoviesServiceCollectionExtencion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICollectService, CollectService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            
            return services;
        }
    }
}
