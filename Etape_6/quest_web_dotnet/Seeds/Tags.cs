using quest_web_dotnet.Models;

namespace quest_web_dotnet.Seeds
{
    public class SeedTags
    {
        public List<Tag> tags = new List<Tag>();
        public SeedTags()
        {
            tags = new List<Tag>()
            {
                new Tag()
                {
                    Name = "Aventure",
                },
                new Tag()
                {
                    Name = "Action",
                },
                new Tag()
                {
                    Name = "Horreur",
                },
                new Tag()
                {
                    Name = "Jeu video",
                },
            };
        }
        public List<Tag> getFakeTags()
        {
            int nbTag = new Random().Next(0, tags.Count);
            List<Tag> ret = new List<Tag>();
            for (int i = 0; i < nbTag; i++)
            {
                int rdm = new Random().Next(0, tags.Count);
                if (!ret.Contains(tags[rdm]))
                {
                    ret.Add(tags[rdm]);
                }
            }
            return ret;
        }
    }
}
