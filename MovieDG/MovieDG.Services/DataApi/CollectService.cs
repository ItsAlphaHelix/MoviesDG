namespace MoviesDG.Core.DataApi
{
    using System;
    using System.Linq;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Data.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Security.Cryptography.X509Certificates;

    public class CollectService : ICollectService
    {
        private const string PosterImageSizePath = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2";
        private const string IMDBMoviePath = "https://www.imdb.com/title/";
        private const string BannerImageSizePath = "https://www.themoviedb.org/t/p/original";

        private readonly IDataService dataService;
        private readonly IRepository<Movie> moviesRepository;
        private readonly IRepository<Genre> genresRepository;
        private readonly IRepository<Language> languagesRepository;
        private readonly IRepository<Country> countriesRepository;
        private readonly IRepository<Actor> actorsRepository;
        public CollectService(
            IDataService dataService,
            IRepository<Movie> moviesRepository,
            IRepository<Genre> genresRepository,
            IRepository<Language> languagesRepository,
            IRepository<Country> contriesRepository,
            IRepository<Actor> actorsRepository)
        {
            this.dataService = dataService;
            this.moviesRepository = moviesRepository;
            this.genresRepository = genresRepository;
            this.languagesRepository = languagesRepository;
            this.countriesRepository = contriesRepository;
            this.actorsRepository = actorsRepository;
        }
        public async Task<int> AddMoviesToDatabaseAsync(int startIndex, int endIndex)
        {
            int addedMovies = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                var movieData = await this.dataService.GetMovieDataAsync(i);

                if (movieData != null &&
                    movieData.Title != null &&
                    movieData.Poster != null &&
                    movieData.IMDBPathId != null &&
                    movieData.Overview != null)
                {
                    var movie = new Movie
                    {
                        TMDBId = movieData.Id,
                        Title = movieData.Title,
                        ReleaseDate = DateTime.ParseExact(movieData.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Poster = $"{PosterImageSizePath}{movieData.Poster}",
                        Banner = $"{BannerImageSizePath}{movieData.Banner}",
                        IMDBLink = $"{IMDBMoviePath}{movieData.IMDBPathId}",
                        Runtime = movieData.Runtime,
                        Overview = movieData.Overview,
                        Popularity = movieData.Popularity,
                        TotalVotes = movieData.TotalVotes,
                        AverageVotes = movieData.AverageVote,
                    };

                    var trailers = await this.dataService.GetMovieTrailersAsync(movieData.Id);

                    var castAndCrew = await this.dataService.GetCastAndCrewAsync(movieData.Id);

                    var director = castAndCrew.Crew.FirstOrDefault(x => x.Job == "Director").Name;

                    foreach (var trailer in trailers.Trailers)
                    {
                        if (trailer.Official is true && trailer.Type == "Trailer")
                        {
                            if (trailer.Name.Contains("Official", StringComparison.OrdinalIgnoreCase) ||
                                trailer.Name.Contains("Main", StringComparison.OrdinalIgnoreCase) ||
                                trailer.Name.Contains("Final", StringComparison.OrdinalIgnoreCase))
                            {
                                movie.Trailer = trailer.Path;
                            }
                        }
                    }


                    foreach (var genre in movieData.Genres)
                    {
                        var targetGenre = await this.genresRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Type == genre.Name);

                        if (targetGenre is null)
                        {
                            targetGenre = new Genre { Type = genre.Name };

                            await this.genresRepository.AddAsync(targetGenre);
                            await this.genresRepository.SaveChangesAsync();
                        }

                        movie.MovieGenres.Add(new MovieGenre { GenreId = targetGenre.Id });
                    }

                    foreach (var language in movieData.Languages)
                    {
                        var targetLanguage = this.languagesRepository.AllAsNoTracking().FirstOrDefault(x => x.LanguageName == language.Name);

                        if (targetLanguage == null)
                        {
                            targetLanguage = new Language { LanguageName = language.Name };

                            await this.languagesRepository.AddAsync(targetLanguage);
                            await this.languagesRepository.SaveChangesAsync();
                        }

                        movie.MovieLanguages.Add(new MovieLanguage { LanguageId = targetLanguage.Id });
                    }

                    foreach (var country in movieData.Countries)
                    {
                        var targetCountry = await this.countriesRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == country.Name);

                        if (targetCountry is null)
                        {
                            targetCountry = new Country { Name = country.Name };

                            await this.countriesRepository.AddAsync(targetCountry);
                            await this.countriesRepository.SaveChangesAsync();
                        }

                        movie.MovieCountries.Add(new MovieCountry { CountryId = targetCountry.Id });
                    }

                    foreach (var cast in castAndCrew.Cast.Take(10))
                    {
                        var currentActor = await this.dataService.GetActorAsync(cast.ActorId);

                        var targetActor = await this.actorsRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == currentActor.Name);

                        if (targetActor is null)
                        {

                            targetActor = new Actor
                            {
                                Name = currentActor.Name,
                            };

                            await this.actorsRepository.AddAsync(targetActor);
                            await this.actorsRepository.SaveChangesAsync();
                        }

                        movie.MovieActors.Add(new MovieActor
                        {
                            ActorId = targetActor.Id,
                            CharacterName = cast.CharacterName,
                        });
                    }

                    await this.moviesRepository.AddAsync(movie);
                    await this.moviesRepository.SaveChangesAsync();
                    addedMovies++;
                }
            }

            return addedMovies;
        }
    }
}
