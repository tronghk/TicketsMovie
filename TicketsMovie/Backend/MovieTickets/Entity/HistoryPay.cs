using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("HistoryPay")]
    public class HistoryPay
    {
        [Key]
        public string UserId { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float Price { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        public string TypePay { get; set; }

    }
}
