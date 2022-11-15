namespace MoviesDG.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MoviesDG.Data.Constants;
    public class Actor
    {
        public Actor()
        {
            this.Movies = new HashSet<MovieActor>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaxActorName)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}