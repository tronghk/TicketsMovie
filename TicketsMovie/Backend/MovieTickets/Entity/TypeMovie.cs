using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("TypeMovie")]
    public class TypeMovie
    {
        [Key]
        public string TypeMovieId { get; set; }
        [Required]
        public string TypeMovieName { get; set;}
        public ICollection<DetailsTypeMovie> Details { get; set; }  
    }
}
