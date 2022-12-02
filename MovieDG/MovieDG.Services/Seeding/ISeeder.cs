namespace MovieDG.Core.Seeding
{
    using MoviesDG.Data;
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(MovieDGDbContext dbContext, IServiceProvider serviceProvider);
    }
}