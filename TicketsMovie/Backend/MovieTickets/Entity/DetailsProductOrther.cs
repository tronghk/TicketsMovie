﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTickets.Entity
{
    [Table("DetailsProductOrther")]
    public class DetailsProductOrther
    {

        public string TicketId { get; set; }


        public string ProductOrtherId {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        [Required]
        public int Quantity { get; set; }
        [Range(1, float.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required]
        public float Total {  get; set; }

        public Ticket Ticket { get; set; }
        public ProductOrther ProductOrther { get; set; }



    }
}
