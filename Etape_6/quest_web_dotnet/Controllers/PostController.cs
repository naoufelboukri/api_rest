using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using System.Text.Json.Nodes;
using System.Linq;
using System.Text.RegularExpressions;
using quest_web_dotnet.Seeds;
using Newtonsoft.Json;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : BaseController<Post>
    {
        public PostController(APIDbContext context, JwtTokenUtil jwt) : base(context, jwt, context.posts) { }
        [HttpGet]
        public override IActionResult getAll(PaginationParameters paginationParameters)
        {
            var posts = from s in _contextName select s;
            var results = posts.Include(p => p.Ratings).Include(p => p.PostTags).ThenInclude(tag => tag.Tag);
            var pagedList = PagedList<Post>.toPagedList(results, paginationParameters.PageNumber, paginationParameters.PageSize);
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };
            return Ok(new { data = pagedList, meta = metadata });
        }

        [HttpGet("search")]
        public IActionResult getBySearch(int page = 1, string search = "")
        {
            int per_page = 10;
            _contextName
                .Include(p => p.Ratings)
                .Include(p => p.PostTags)
                .ThenInclude(tag => tag.Tag)
                .ToList();
            return Ok(_contextName.Skip((page - 1) * per_page).Take(per_page).Where(p => p.Title.Contains(search)).ToList());
        }

        [HttpGet("{id}")]
        public override IActionResult get(int id)
        {
            Post? post = _contextName.Find(id);
            if (post == null)
            {
                return BadRequest(errorMessageExist(id));
            }
            _contextName.Include(p => p.Ratings).Include(p => p.PostTags).ThenInclude(tag => tag.Tag).ToList();
            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromHeader] string Authorization, [FromBody] PostBody request)
        {
            User? user = getUser(Authorization);
            if (user != null)
            {
                Post post = new Post
                {
                    Title = request.Title,
                    Content = request.Content,
                    UserId = user.Id,
                    Updated_At = DateTime.Now
                };

                List<PostTag> tags = new List<PostTag>();
                if (request.Tags != "")
                {
                    string[] tagsId = request.Tags.Split(',');
                    foreach (string id in tagsId)
                    {
                        Tag? tag = _context.tags.Find(int.Parse(id));
                        if (tag != null)
                        {
                            tags.Add(new PostTag { Post = post, Tag = tag });
                        }
                    }
                    post.PostTags = tags;
                }
                _contextName.Add(post);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Create), post);
            }
            return StatusCode(403, unauthorizeMessage);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit([FromHeader] string Authorization, JsonObject request, int id)
        {
            User? user = getUser(Authorization);
            if (user != null)
            {
                Post? post = _contextName.Find(id);
                if (post != null)
                {
                    if (post.UserId == user.Id || user.Role == "ROLE_ADMIN")
                    {
                        post.Title = (string)(request.ContainsKey("title") ? request["title"] : post.Title);
                        post.Content = (string)(request.ContainsKey("content") ? request["content"] : post.Content);
                        post.Updated_At = DateTime.Now;
                        _context.SaveChanges();
                        return Ok(post);
                    }
                    return StatusCode(403, unauthorizeMessage);
                }
                return BadRequest(errorMessageExist(id));
            }
            return StatusCode(403, unauthorizeMessage);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> Generate()
        {
            SeedTags seedTags = new SeedTags();
            SeedPosts seedPosts = new SeedPosts();

            int userId = _context.users.FirstOrDefault(user => user.Id > 0).Id;
            var posts = seedPosts.getFakePosts(userId);
            var allTags = seedTags.tags;

            //foreach (var tag in allTags)
            //{
            //    if (_context.tags.FirstOrDefault(t => t.Name == tag.Name) == null)
            //    {
            //        _context.tags.Add(tag);
            //    }
            //}

            for (int i = 0; i < posts.Count;i++)
            {
                List<PostTag> postTags = new List<PostTag>();

                Post post = posts[i];
                List<Tag> tags = seedTags.getFakeTags();
                foreach (var tag in tags)
                {
                    postTags.Add(new PostTag { Tag = tag, Post = post });
                }
                post.PostTags = postTags;
                _contextName.Add(post);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "Données générées" });
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            foreach(var entity in _contextName)
            {
                _context.Remove(entity);
            }
            _context.SaveChanges();
            return Ok(new { message = "Posts supprimés" });
        }

        public class SearchRequest
        {
            public string search { set; get; }
        }
    }
}
