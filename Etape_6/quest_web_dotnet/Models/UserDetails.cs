using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace quest_web.Models
{
    public class UserDetails
    {
        [Required]
        [MaxLength(255)]
        [Column("username")]
        public string Username { get; set; }

        [AllowNull]
        [MaxLength(255)]
        [Column("role")]
        public string? Role { get; set; }

        public UserDetails(string username, string role)
        {
            this.Username = username;
            this.Role = role;
        }
    }
}
