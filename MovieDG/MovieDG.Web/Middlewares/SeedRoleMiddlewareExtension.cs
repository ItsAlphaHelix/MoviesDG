using MovieDG.Core.Seeding;
using MoviesDG.Data;

namespace MovieDG.Web.Middlewares
{
    public static class SeedRoleMiddlewareExtension
    {
        public static WebApplication Seed(
        this WebApplication app)
        {
            var serviceScope = app.Services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDGDbContext>();
             new MovieDGDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();

            return app;
        }
    }
}
