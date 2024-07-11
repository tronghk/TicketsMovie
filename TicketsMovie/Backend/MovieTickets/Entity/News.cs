using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("News")]
    public class News
    {

        [Key]
        [Required]
        public string NewId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Author { get; set; }
    }
}
