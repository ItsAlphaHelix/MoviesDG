namespace MoviesDG.Data.Models
{
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class MovieActor
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;


        [ForeignKey(nameof(Actor))]
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.MaxActorCharacterName)]
        public string CharacterName { get; set; } = null!;
    }
}
