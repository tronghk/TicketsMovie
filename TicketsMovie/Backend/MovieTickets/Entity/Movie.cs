using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZingMp3API.Data;

namespace MovieTickets.Entity
{
    [Table("Movie")]
    public class Movie
    {

        [Key]
        public string MovieId { get; set; }
        [Required]
        public string MovieName { get; set;}
        [Required]
        public int MovieTime {  get; set; }
        [Required]
        public int Status { get; set;}
        [Required]
        public DateTime DateStart { get; set;}

        [Required]
        public string Image {  get; set;}

        [Required]
        public string Dicrectory { get; set;}
        [Required]
        public string Performer { get; set;}

        public string LanguageMovieId
        { get; set; } = null!;
        //lưu vào foreign key
        [ForeignKey("LanguageMovieId")]
        // class ánh xạ
        public virtual LanguageMovie? LanguageMovie { get; set; }

        public ICollection<DetailsTicket> DetailsTickets { get; set; }

        public ICollection<DetailsTypeMovie> DetailsTypeMovie { get; set; }

    }
}
