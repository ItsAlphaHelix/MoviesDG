namespace MoviesDG.Data.Models
{
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    public class Language
    {
        public Language()
        {
            this.Movies = new HashSet<MovieLanguage>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxLanguageName)]
        public string LanguageName { get; set; } = null!;

        public virtual ICollection<MovieLanguage> Movies { get; set; }
    }
}