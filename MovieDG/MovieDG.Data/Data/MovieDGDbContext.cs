namespace MoviesDG.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Data.Models;
    using MoviesDG.Data.Models;
    public class MovieDGDbContext : IdentityDbContext
    {
        public MovieDGDbContext(DbContextOptions<MovieDGDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        //TODO: Add this entitiy after creating ApplicationUser entitiy!

        //public DbSet<Comment> Comments { get; set; }

        //public DbSet<MovieComment> MovieComments { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }

        public DbSet<MovieGenre> MoviesGenres { get; set; }

        public DbSet<MovieLanguage> MoviesLanguages { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<MovieCountry> MoviesCountries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId });

            builder.Entity<MovieGenre>()
                .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<MovieLanguage>()
                .HasKey(x => new { x.MovieId, x.LanguageId });

            builder.Entity<MovieCountry>()
                .HasKey(x => new { x.MovieId, x.CountryId });

            base.OnModelCreating(builder);
        }
    }
}