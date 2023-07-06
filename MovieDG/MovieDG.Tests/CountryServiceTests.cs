namespace MovieDG.Tests
{
    using MovieDG.Core.Contracts;
    using MovieDG.Core.Services;
    using MovieDG.Tests.Seed;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;

    public class CountryServiceTests
    {
        private ICountryService countryService;
        private EfRepository<Country> countryRepository;

        [SetUp]
        public void SetUp()
        {
            countryRepository = InMemoryDatabaseSetup.SetupWithoutUserRepo<Country>();
            countryService = new CountryService(countryRepository);
        }

        [Test]
        public async Task GetAllCountriesAsyncTest()
        {
            await SeedDB.SeedCountries(countryRepository);

            var countries = await this.countryService.GetAllCountriesAsync();

            Assert.That(countries.Count(), Is.EqualTo(10));
        }

        [TearDown]
        public void Dispose()
        {
            InMemoryDatabaseSetup.InMemoryDatabaseDispose();
        }
    }
}
