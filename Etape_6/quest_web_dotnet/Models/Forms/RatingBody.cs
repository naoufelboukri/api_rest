namespace quest_web_dotnet.Models.Forms
{
    public class RatingBody
    {
        public string Content { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
