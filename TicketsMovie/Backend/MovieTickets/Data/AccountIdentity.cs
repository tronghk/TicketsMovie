using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZingMp3API.Data
{
    public class AccountIdentity : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
