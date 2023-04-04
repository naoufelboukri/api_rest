using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quest_web.Models;

public enum UserRole
{
    ROLE_USER,
    ROLE_ADMIN
}

public class User {
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
    public string Role { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime creation_date { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime updated_date { get; set; }

    public User(string username, string password) {
        this.Id = Guid.NewGuid();
        this.Username = username;
        this.Password = password.GetHashCode().ToString();
        this.Role = UserRole.ROLE_USER.ToString();
        this.creation_date = DateTime.Now;
        this.updated_date = DateTime.Now;
    }
}