using MovieTickets.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public string TicketsId
        { get; set; }
        public DateTime DateCreate { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public float Money { get; set; }

        public string Description { get; set; }

        public string UserId
        { get; set; } = null!;
        //lưu vào foreign key
        [ForeignKey("UserId")]
        // class ánh xạ
        public virtual UserInfor? UserInfor { get; set; }

        public ICollection<DetailsTicket> DetailsTickets { get; set; }

        public ICollection<DetailsProductOrther> DetailsProductOrthers { get; set; }

    }
}
