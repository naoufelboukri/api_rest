using System.ComponentModel.DataAnnotations;

namespace quest_web.Models;

public enum UserRole
{
    ROLE_USER,
    ROLE_ADMIN
}

public class User
{
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(255)]
    public string username { get; set; }

    [Required]
    [MaxLength(255)]
    public string password { get; set; }
    public string role { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime creation_date { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime updated_date { get; set; }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password.GetHashCode().ToString();
        this.role = UserRole.ROLE_USER.ToString();
        this.creation_date = DateTime.Now;
        this.updated_date = DateTime.Now;
    }
}
