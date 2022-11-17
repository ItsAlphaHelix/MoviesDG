﻿namespace MovieDG.Core.Contracts
{
using MovieDG.Core.ViewModels.Countries;
    public interface ICountryService
    {
        IEnumerable<CountryViewModel> GetAllCountries();
    }
}
