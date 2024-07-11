using MovieTickets.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZingMp3API.Data;

namespace MovieTickets.Data
{
    [Table("UserInfor")]
    public class UserInfor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }

        [Required]
        [Key]
        //định nghĩa kiểu dữ liệu khóa ngoại
       
        public string UserId { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
