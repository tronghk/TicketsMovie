using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("DetailsTypeMovie")]
    public class DetailsTypeMovie
    {
        [Required]
        public string MovieId { get; set; }
        [Required]
        public string TypeMovieId { get; set; }


        public Movie movie { get; set; }

        public TypeMovie TypeMovie { get; set; }
    }
}
