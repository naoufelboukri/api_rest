using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("post")]
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

        [HttpGet]
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
            var username = new JwtService(_jwt).getUsername(Authorization);
            if (username != null) 
            {
                var user = _context.users.FirstOrDefault(user => user.Username == username);
                if ( user != null )
                {
                    var post = new Post
                    {
                        Title = request.Title,
                        Content = request.Content
                    };
                    _context.posts.Add(post);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(newPost), post);
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }

    }
}
