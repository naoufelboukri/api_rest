using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("tags")]
    public class TagController : BaseController<Tag>
    {
        public TagController(APIDbContext context, JwtTokenUtil jwt) : base(context, jwt, context.tags) { }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] TagBody request, [FromHeader] string Authorization)
        {
            User? user = getUser(Authorization);
            if (user != null && user.Role == "ROLE_ADMIN")
            {
                if (_contextName.FirstOrDefault(tag => tag.Name == request.Name) == null)
                {
                    Tag tag = new Tag { Name = request.Name };
                    _contextName.Add(tag);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Create), tag);
                }
                return BadRequest(new { message = "Cette catégorie existe déjà" });
            }
            return StatusCode(403, unauthorizeMessage);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult editTag([FromHeader] string Authorization, JsonObject request, int id)
        {
            User? user = getUser(Authorization);
            Tag? tag = _contextName.Find(id);
            if (user != null && user.Role == "ROLE_ADMIN")
            {
                if (tag != null)
                {
                    tag.Name = (string)(request.ContainsKey("name") ? request["name"] : tag.Name);
                    _context.SaveChanges();
                    return Ok(tag);
                }
                return BadRequest(errorMessageExist(id));
            }
            return StatusCode(403, unauthorizeMessage);
        }
    }
}
