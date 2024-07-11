using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("AccumulationAccount")]
    public class AccumulationAccount
    {
        [Key]
        public string UserId {  get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float Total {  get; set; }


    }
}
