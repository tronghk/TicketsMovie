using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("DetailsTicket")]
    public class DetailsTicket
    {
        public string TicketId { get; set; }

        public string MovieId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        [Required]
        public int Quantity { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float PriceTicket { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float Discount {  get; set; }
        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float Total {  get; set; }

        [Required]
        public string Seat { get; set; }

        public Ticket Ticket { get; set; }
        public Movie Movie { get; set; }

    }
}
