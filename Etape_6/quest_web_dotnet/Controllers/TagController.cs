using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("tags")]
    public class TagController : Controller
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;

        public TagController(APIDbContext context, JwtTokenUtil jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpGet]
        public IActionResult getTags()
        {
            return Ok(_context.tags.ToArray());
        }

        [HttpGet("{id}")]
        public IActionResult getTag(int id)
        {
            var tag = _context.tags.FirstOrDefault(tag => tag.Id == id);
            if (tag == null)
            {
                return BadRequest(new { message = "Ce tag n'existe pas" });
            }
            return Ok(new { id = tag.Id, name = tag.Name });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createTask([FromBody] TagBody request, [FromHeader] string Authorization)
        {
            var username = new JwtService(_jwt).getUsername(Authorization);
            if (username != null)
            {
                var user = _context.users.FirstOrDefault(user => user.Username == username);
                if (user != null && user.Role == "ROLE_ADMIN")
                {
                    if (_context.tags.FirstOrDefault(tag => tag.Name == request.Name) == null)
                    {
                        var task = new Models.Tag
                        {
                            Name = request.Name,
                        };
                        _context.tags.Add(task);
                        await _context.SaveChangesAsync();
                        return CreatedAtAction(nameof(createTask), task);
                    }
                    return BadRequest(new { message = "Ce tag existe déjà " });
                }
                
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult editTag([FromHeader] string Authorization, JsonObject request, int id)
        {
            var username = new JwtService(_jwt).getUsername(Authorization);
            if (username != null)
            {
                var user = _context.users.FirstOrDefault(u => u.Username == username);
                if (user != null && user.Role == "ROLE_ADMIN")
                {
                    var tag = _context.tags.FirstOrDefault(tag => tag.Id == id);
                    if (tag == null)
                    {
                        return BadRequest(new { message = "Ce tag n'existe pas " });
                    }
                    tag.Name = (string)(request.ContainsKey("name") ? request["name"] : tag.Name);
                    _context.SaveChanges();
                    return Ok(tag);
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }
    }
}
