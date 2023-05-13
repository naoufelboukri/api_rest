using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : Controller
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;

        public PostController(APIDbContext context, JwtTokenUtil jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpGet]
        public IActionResult getPosts()
        {
            var posts = _context.posts.ToArray();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult post(int id)
        {
            var post = _context.posts.FirstOrDefault(post => post.Id == id);
            if (post == null)
            {
                return BadRequest(new { message = "Ce post n'existe pas" });
            }
            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> newPost([FromBody] PostBody request, [FromHeader] string Authorization)
        {
            var username = new JwtService(_jwt, _context).getUsername(Authorization);
            if (username != null) 
            {
                var user = _context.users.FirstOrDefault(user => user.Username == username);
                if ( user != null )
                {
                    var post = new Post
                    {
                        Title = request.Title,
                        Content = request.Content,
                        UserId = user.Id,
                    };
                    _context.posts.Add(post);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(newPost), post);
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult editPost([FromHeader] string Authorization, JsonObject request, int id)
        {
            var username = new JwtService(_jwt, _context).getUsername(Authorization);
            if (username != null)
            {
                var user = _context.users.FirstOrDefault(u => u.Username == username);
                if ( user != null )
                {
                    var post = _context.posts.FirstOrDefault(post => post.Id == id);
                    if ( post != null && (post.UserId == user.Id || user.Role == "ROLE_ADMIN"))
                    {
                        post.Title = (string)(request.ContainsKey("title") ? request["title"] : post.Title);
                        post.Content = (string)(request.ContainsKey("content") ? request["content"] : post.Content);
                        post.UpdatedDate = DateTime.Now;
                        _context.SaveChanges();
                        return Ok(post);
                    }
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult deletePost([FromHeader] string Authorization, int id)
        {
            var user = new JwtService(_jwt, _context).getUser(Authorization);
            if (user != null)
            {
                var post = _context.posts.FirstOrDefault(p => p.Id == id);
                if (post.UserId == user.Id || user.Role == "ROLE_ADMIN")
                {
                    _context.posts.Remove(post);
                    _context.SaveChanges();
                    return Ok(new { message = "Le post as bien été supprimé " });
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }
    }
}
