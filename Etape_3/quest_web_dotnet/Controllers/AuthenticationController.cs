using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using quest_web.Models.Form;

namespace quest_web.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly APIDbContext _context;
        public AuthenticationController(APIDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterBody request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
                {
                    return BadRequest(new { message = "username and password mandatory" });
                }
                if (await _context.user.AnyAsync(u => u.Username == request.username))
                {
                    return Conflict(new { message = "Le nom d'utilisateur est déjà utilisé" });
                }

                var user = new User(request.username, request.password);
                _context.Add(user);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(register), new UserDetails(user.Username, user.Role));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
