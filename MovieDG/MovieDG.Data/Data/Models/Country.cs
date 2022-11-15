namespace MoviesDG.Data.Models
{
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    public class Country
    {
        public Country()
        {
            this.MovieCounties = new HashSet<MovieCountry>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxCountryName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<MovieCountry> MovieCounties { get; set; }
    }
}
