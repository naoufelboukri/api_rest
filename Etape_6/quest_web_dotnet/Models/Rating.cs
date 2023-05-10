using Microsoft.EntityFrameworkCore.Metadata.Internal;
using quest_web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace quest_web_dotnet.Models
{
    public class Rating
    {
        [Key]
        [Column(("id"))]
        public int Id { get; set; }

        [AllowNull]
        [Column("content")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [AllowNull]
        [DataType(DataType.DateTime)]
        [Column("creation_date", TypeName = "DateTime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreationDate { get; set; }

        [Column("rating")]
        [Required]
        public int Rating { get; set; }


        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }

        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
