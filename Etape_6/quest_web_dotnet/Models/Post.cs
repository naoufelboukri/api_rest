using quest_web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;

namespace quest_web_dotnet.Models
{
    public class Post : IEntity
    {
        [Key]
        [Column(("id"))]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }

        [AllowNull]
        [DataType(DataType.DateTime)]
        [Column("created_at", TypeName = "DateTime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Created_At { get; set; }

        [AllowNull]
        [DataType(DataType.DateTime)]
        [Column("updated_at", TypeName = "DateTime")]
        public DateTime? Updated_At { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        [AllowNull]
        public ICollection<PostTag> PostTags { get; set; }
    }
}
