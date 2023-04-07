namespace MovieDG.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using System.Diagnostics.Metrics;

    public class CountryServiceTests
    {
        private ICountryService countryService;
        private EfRepository<Country> countryRepository;
        private MovieDGDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDataBase();
            countryService = new CountryService(countryRepository);
        }

        [Test]
        public async Task GetAllCountriesAsyncTest()
        {
            await SeedDB.SeedCountries(countryRepository);

            var countries = await this.countryService.GetAllCountriesAsync();

            Assert.That(countries.Count(), Is.EqualTo(10));
        }
        private void SetupInMemoryDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<MovieDGDbContext>()
                              .UseInMemoryDatabase("MoviesDG")
                              .Options;

            dbContext = new MovieDGDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            countryRepository = new EfRepository<Country>(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
