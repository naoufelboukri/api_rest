using quest_web_dotnet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace quest_web.Models;

public enum UserRole
{
    ROLE_USER,
    ROLE_ADMIN
}

public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("password")]
    public string Password { get; set; }

    [MaxLength(255)]
    [Column("role")]
    public string? Role { get; set; }

    [DataType(DataType.DateTime)]
    [AllowNull]
    [Column("creation_date", TypeName = "DateTime")]
    public DateTime? CreationDate { get; set; }

    [AllowNull]
    [Column("updated_date", TypeName = "DateTime")]
    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();



    public string getHashCode()
    {
        return BCrypt.Net.BCrypt.HashPassword(this.Password);
    }
}