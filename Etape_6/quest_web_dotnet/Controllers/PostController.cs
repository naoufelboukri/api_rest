using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : BaseController<Post>
    {
        public PostController(APIDbContext context, JwtTokenUtil jwt) : base(context, jwt, context.posts) { }

        public override IActionResult getAll()
        {
            _contextName.Include(p => p.Ratings).Include(p => p.PostTags).ThenInclude(tag => tag.Tag).ToList();
            return Ok(_contextName.ToArray());
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

    }
}
