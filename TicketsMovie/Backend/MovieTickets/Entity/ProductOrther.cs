using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("ProductOrther")]
    public class ProductOrther
    {
        [Key]
        public string ProductOrtherId { get; set; }
        [Required]
        public string ProductOrtherName { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public float Price { get; set; }
        [Required]
        public string Status { get; set; }

        public ICollection<DetailsProductOrther> DetailsProducts { get; set; }  
    }
}
