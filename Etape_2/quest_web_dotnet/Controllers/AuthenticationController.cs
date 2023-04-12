using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web.Models;

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
        public async Task<ActionResult<User>> register(string username, string password)
        {
            try
            {
                if (await _context.user.AnyAsync(u => u.Username == username))
                {
                    return Conflict(new { message = "Le nom d'utilisateur est déjà utilisé" });
                }

                var user = new User(username, password);
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(register), user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
