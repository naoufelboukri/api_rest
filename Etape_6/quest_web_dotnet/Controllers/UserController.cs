using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web.Models;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : BaseController<User>
    {
        public UserController(APIDbContext context, JwtTokenUtil jwt) : base(context, jwt, context.users) { }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit([FromHeader] string Authorization, JsonObject request, int id)
        {
            User? connectedUser = getUser(Authorization);

            if (connectedUser != null && (connectedUser.Role == "ROLE_ADMIN" || connectedUser.Id == id))
            {
                User? targetUser = _contextName.Find(id);
                if (targetUser != null)
                {
                    targetUser.Username = (string)(request.ContainsKey("username") ? request["username"] : targetUser.Username);
                    targetUser.Role = (string)(request.ContainsKey("role") ? request["role"] : targetUser.Role);
                    targetUser.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new UserDetails(targetUser.Username, targetUser.Role));
                }
                return BadRequest(errorMessageExist(id));
            }
            return StatusCode(403, unauthorizeMessage);
        }
    }
}
