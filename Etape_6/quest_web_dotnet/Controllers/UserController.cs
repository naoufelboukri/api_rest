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
    public class UserController : Controller
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;

        public UserController(APIDbContext context, JwtTokenUtil jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpGet]
        public IActionResult getUsers()
        {
            var users = _context.users.Select(user => new { id = user.Id, username = user.Username, role = user.Role }).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult getUser(int id)
        {
            var user = _context.users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return BadRequest(new { message = "L'utilisateur n'existe pas" });
            }
            return Ok(new
            {
                username = user.Username,
                role = user.Role
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult editUser([FromHeader] string Authorization, JsonObject request, int id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.users.ToList().FirstOrDefault(user => (user.Username == username));
                if (user == null)
                {
                    return Unauthorized(new { message = $"L'utilisateur {id} n'existe pas " });
                }
                if (user.Role == "ROLE_USER")
                {
                    var userDetails = _context.users.FirstOrDefault(u => (u.Id == id));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { message = "Cette utilisateur n'existe pas ou ne vous n'avez pas les droits" });
                    }
                    userDetails.Username = (string)(request.ContainsKey("username") ? request["username"] : userDetails.Username);
                    userDetails.Role = (string)(request.ContainsKey("role") ? request["role"] : userDetails.Role);
                    userDetails.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(new UserDetails(userDetails.Username, userDetails.Role));
                }
                else if (user.Role == "ROLE_ADMIN")
                {
                    var userDetails = _context.users.FirstOrDefault(user => (user.Id == id));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { message = "Cette utilisateur n'existe pas" });
                    }
                    userDetails.Username = (string)(request.ContainsKey("username") ? request["username"] : userDetails.Username);
                    userDetails.Role = (string)(request.ContainsKey("role") ? request["role"] : userDetails.Role);
                    userDetails.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(new UserDetails(userDetails.Username, userDetails.Role));
                }
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult deleteUser([FromHeader] string Authorization, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.users.FirstOrDefault(user => (user.Username == username));

                if (user == null)
                {
                    return Unauthorized(new { success = "false", message = $"L'utilisateur {id} n'existe pas " });
                }

                if (user.Role == "ROLE_USER")
                {
                    var userDetails = _context.users.FirstOrDefault(u => (u.Id == int.Parse(id) && u.Id == user.Id));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { success = "false", message = "L'utilisateur n'existe pas" });
                     }
                    _context.Remove(userDetails);
                    _context.SaveChanges();
                    return Ok(new { success = "true", message = "L'utilisateur a bien été supprimé" });
                }
                else if (user.Role == "ROLE_ADMIN")
                {
                    var userDetails = _context.users.FirstOrDefault(user => (user.Id == int.Parse(id)));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { success = "false", message = "L'utilisateur n'existe pas" });
                    }
                    _context.Remove(userDetails);
                    _context.SaveChanges();
                    return Ok(new { success = "true", message = "L'utilisateur a bien été supprimé" });
                }
            }
            return BadRequest(new { success = "false", message = "Vous n'avez pas les droits" });
        }
    }
}
