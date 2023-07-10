namespace MovieDG.Tests.Seed
{
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Models;
    using MoviesDG.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public static class SeedDB
    {
        public static async Task SeedMovies(EfRepository<Movie> movieRepository)
        {

            for (int i = 0; i < 30; i++)
            {
                List<MovieGenre> genres = new List<MovieGenre>();

                genres.Add(new MovieGenre()
                {
                    Genre = new Genre()
                    {
                        Type = "Fantasy" + i
                    }
                });

                List<MovieCountry> countries = new List<MovieCountry>();

                countries.Add(new MovieCountry()
                {
                    Country = new Country()
                    {
                        Name = "Bulgaria" + i
                    }
                });

                List<MovieActor> actors = new List<MovieActor>();

                actors.Add(new MovieActor()
                {
                    CharacterName = "",
                    Actor = new Actor()
                    {
                        Name = "Actor" + i
                    }
                });

                Movie movie = new Movie()
                {
                    Id = 46 + i,
                    Banner = "",
                    IMDBLink = "",
                    Overview = "",
                    Poster = "",
                    Title = "John Wick" + i,
                    Trailer = "",
                    AverageVotes = i + 2.5,
                    Popularity = i + 3,
                    ReleaseDate = new DateTime(1994 + i, 12, i + 1),
                    MovieGenres = new List<MovieGenre>(genres),
                    MovieCountries = new List<MovieCountry>(countries),
                    MovieActors = new List<MovieActor>(actors)
                };

                await movieRepository.AddAsync(movie);
            }

            await movieRepository.SaveChangesAsync();
        }

        public static async Task SeedUsers(EfRepository<ApplicationUser> userRepository)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Test",
                Email = "test@gmail.com",
                Country = "Testomania",
                PhoneNumber = "0553322012",
                City = "Testo"
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
        }

        public static async Task SeedCountries(EfRepository<Country> countryRepository)
        {
            for (int i = 0; i < 10; i++)
            {
                var country = new Country()
                {
                    Id = i + 1,
                    Name = "Bulgaria"
                };

                await countryRepository.AddAsync(country);
            }

            await countryRepository.SaveChangesAsync();
        }

        public static async Task SeedGenres(EfRepository<Genre> genreRepository)
        {
            for (int i = 0; i < 10; i++)
            {
                var genre = new Genre()
                {
                    Id = i + 1,
                    Type = "Fantasy"
                };

                await genreRepository.AddAsync(genre);
            }

            await genreRepository.SaveChangesAsync();
        }

        public static async Task SeedMessages(EfRepository<Chat> chatRepository)
        {
            for (int i = 0; i < 10; i++)
            {
                var chat = new Chat()
                {
                    Id = i + 1,
                    Text = $"Hello + {i}",
                    Name = $"Alpha + {i}"
                };

                await chatRepository.AddAsync(chat);
            }

            await chatRepository.SaveChangesAsync();
        }
    }
}
