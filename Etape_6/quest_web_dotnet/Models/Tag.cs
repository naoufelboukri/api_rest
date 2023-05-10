using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace quest_web_dotnet.Models
{
    public class Tag
    {
        [Key]
        [Column(("id"))]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

    }
}
