using System.Text.Json.Serialization;

namespace quest_web_dotnet.Models
{
    public class PostTag
    {
        [JsonIgnore]
        public int PostsId { get; set; }
        [JsonIgnore]
        public int TagsId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
