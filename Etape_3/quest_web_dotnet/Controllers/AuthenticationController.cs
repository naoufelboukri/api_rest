using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using quest_web.Models.Form;
using quest_web.Utils;
using System.Net.Http.Headers;

namespace quest_web.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;
        public AuthenticationController(APIDbContext context, JwtTokenUtil JWT)
        {
            _context = context;
            _jwt = JWT;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] UserBody request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
                {
                    return BadRequest(new { message = "Le nom d'utilisateur et le mot de passe sont obligatoires" });
                }
                if (await _context.user.AnyAsync(u => u.Username == request.username))
                {
                    return Conflict(new { message = "Le nom d'utilisateur est déjà utilisé" });
                }

                var user = new User(request.username, request.password);
                user.Password = user.getHashCode();
                _context.Add(user);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(register), new UserDetails(user.Username, user.Role));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> authenticate([FromBody] UserBody request)
        {
            var user = _context.user.ToList().FirstOrDefault(user => user.Username == request.username);
            if (user == null)
            {
                return StatusCode(401,(new { message = "Identifiant ou mot de passe incorrect" }));
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, user.Password))
            {
                return StatusCode(401,(new { messagee = "Identifiant ou mot de passe incorrect" }));
            }

            var token = this._jwt.GenerateToken(new UserDetails(user.Username, user.Role));
            return Ok(new {token = token});
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult me([FromHeader] string Authorization)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;

                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));
                if (user == null)
                {
                    return BadRequest(new { message = "L'utilisateur n'existe pas" });
                }
                return Ok(new UserDetails(user.Username, user.Role));
            }
            return Unauthorized(new { message = "ddede" });
        }
    }
}


