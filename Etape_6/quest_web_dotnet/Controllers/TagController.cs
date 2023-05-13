using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Net.Http.Headers;

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createTask([FromBody] TagBody request, [FromHeader] string Autorization)
        {
            var username = new JwtService(_jwt).getUsername(Autorization);
            if (username != null)
            {
                var user = _context.users.FirstOrDefault(user => user.Username == username);
                if (user != null && user.Role == "ROLE_ADMIN")
                {
                    var task = new Models.Tag
                    {
                        Name = request.Name,
                    };
                    _context.tags.Add(task);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(createTask), task);
                }
                
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }
    }
}
