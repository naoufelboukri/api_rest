using System.ComponentModel.DataAnnotations;

namespace quest_web.Models;

public enum UserRole
{
    ROLE_USER,
    ROLE_ADMIN
}

public class User {
    [Key]
    public Guid Id { get; }
    
    [Required]
    [MaxLength(255)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
    public string Role { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; }

    public User(string username, string password) {
        this.Username = username;
        this.Password = password.GetHashCode().ToString();
        this.Role = UserRole.ROLE_USER.ToString();
        this.CreationDate = DateTime.Now;
        this.UpdatedDate = DateTime.Now;
    }
}