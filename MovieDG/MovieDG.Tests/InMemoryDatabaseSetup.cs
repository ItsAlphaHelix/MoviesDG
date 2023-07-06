namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data;
    using MoviesDG.Data.Repositories;
    public static class InMemoryDatabaseSetup
    {
        private static MovieDGDbContext dbContext;
        public static (EfRepository<T>, EfRepository<ApplicationUser>) SetupWithUserRepo<T>()
            where T : class
        {
            CreateInMemoryDatabase();

            var repository = new EfRepository<T>(dbContext);
            var userRepository = new EfRepository<ApplicationUser>(dbContext);

            return (repository, userRepository);
        }


        public static EfRepository<T> SetupWithoutUserRepo<T>()
            where T : class
        {
            CreateInMemoryDatabase();

            var repository = new EfRepository<T>(dbContext);

            return repository;
        }
        private static void CreateInMemoryDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                                .UseInMemoryDatabase("MoviesDG")
                                .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
        public static void InMemoryDatabaseDispose()
        {
            dbContext.Dispose();
        }
    }
}
