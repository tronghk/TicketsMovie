using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("LanguageMovie")]
    public class LanguageMovie
    {
        [Key]
        public string LanguageMovieId { get; set; }

        [Required]
        public string LanguageMovieName { get; set; }

        public ICollection<Movie> Movies { get; set; } = null!;

    }
}
