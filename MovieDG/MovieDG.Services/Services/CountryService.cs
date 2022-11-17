namespace MovieDG.Core.Services
{
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ViewModels.Countries;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using System;
    using System.Collections.Generic;
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountryService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }
        public IEnumerable<CountryViewModel> GetAllCountries()
        {
            var countries = this.countriesRepository
                .AllAsNoTracking()
                .Select(x => new CountryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            return countries;
        }
    }
}
