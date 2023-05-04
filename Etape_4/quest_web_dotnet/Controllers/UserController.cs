using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using quest_web.Models;
using quest_web.Models.Form;
using quest_web.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace quest_web.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;

        public UserController(APIDbContext context, JwtTokenUtil jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult allUsers([FromHeader] string Authorization)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var userList = _context.user.Select(u => new { id = u.Id, username = u.Username, role = u.Role, addresses = u.Addresses }).ToList();
                return Ok(userList);
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public IActionResult user(string id, [FromHeader] string Authorization)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var user = _context.user.FirstOrDefault(user => user.Id == int.Parse(id));
                if (user == null)
                {
                    return BadRequest(new { message = "L'utilisateur n'existe pas" });
                }
                _context.user.Include(x => x.Addresses).ToList();
                return Ok(new
                {
                    username = user.Username,
                    role = user.Role,
                    addresses = user.Addresses
                });
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }

        [HttpPut("user/{id}")]
        [Authorize]
        public IActionResult putUser([FromHeader] string Authorization, JsonObject request, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));
                if (user == null )
                {
                    return Unauthorized(new { message = $"L'utilisateur {id} n'existe pas " });
                }
                if (user.Role == "ROLE_USER")
                {
                    var userDetails = _context.user.FirstOrDefault(u => (u.Id == int.Parse(id)));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { message = "Cette utilisateur n'existe pas ou ne vous n'êtes pas le propriétaire" });
                    }
                    userDetails.Username = (string)(request.ContainsKey("username") ? request["username"] : userDetails.Username);
                    userDetails.Role = (string)(request.ContainsKey("role") ? request["role"] : userDetails.Role);
                    userDetails.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(new UserDetails(userDetails.Username, userDetails.Role));
                }
                else if (user.Role == "ROLE_ADMIN")
                {
                    var userDetails = _context.user.FirstOrDefault(user => (user.Id == int.Parse(id)));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { message = "Cette utilisateur n'existe pas ou ^vous n'êtes pas le propriétaire" });
                    }
                    userDetails.Username = (string)(request.ContainsKey("username") ? request["username"] : userDetails.Username);
                    userDetails.Role = (string)(request.ContainsKey("role") ? request["role"] : userDetails.Role);
                    userDetails.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(new UserDetails(userDetails.Username, userDetails.Role));
                }
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }


        [HttpDelete("user/{id}")]
        [Authorize]
        public IActionResult deleteUser([FromHeader] string Authorization, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.user.FirstOrDefault(user => (user.Username == username));

                if (user.Role == "ROLE_USER")
                {
                    var userDetails = _context.user.FirstOrDefault(u => (u.Id == int.Parse(id) && u.Id == user.Id));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { success = "false" });
                    }
                    _context.Remove(userDetails);
                    _context.SaveChanges();
                    return Ok(new { success = "true" });
                }
                else if (user.Role == "ROLE_ADMIN")
                {
                    var userDetails = _context.user.FirstOrDefault(user => (user.Id == int.Parse(id)));
                    if (userDetails == null)
                    {
                        return StatusCode(403, new { success = "false" });
                    }
                    _context.Remove(userDetails);
                    _context.SaveChanges();
                    return Ok(new { success = "true" });
                }
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }
    }
}
