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
    
    [AllowNull]
    [MaxLength(255)]
    [Column("role")]
    public string ? Role { get; set; }

    [DataType(DataType.DateTime)]
    [AllowNull]
    [Column("creation_date")]
    public DateTime ? CreationDate { get; set; }
    
    [AllowNull]
    [DataType(DataType.DateTime)]
    [Column("updated_date")]
    public DateTime ? UpdatedDate { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password.GetHashCode().ToString();
        this.Role = UserRole.ROLE_USER.ToString();
        this.CreationDate = DateTime.Now;
        this.UpdatedDate = DateTime.Now;
    }
}
