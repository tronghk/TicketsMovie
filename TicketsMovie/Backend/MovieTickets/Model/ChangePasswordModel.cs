using System.ComponentModel.DataAnnotations;

namespace ZingMp3API.Model
{
    public class ChangePasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string OldPassword { get; set; } = null!;
    }
}
