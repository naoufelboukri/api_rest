using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web.Models;

namespace quest_web.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly APIDbContext _context;
        public AuthentificationController (APIDbContext context) {
            _context = context;    
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> register(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username)) {
                return BadRequest("Le nom d'utilisateur est déjà utilisé");
            }
            var user = new User(username, password);
            _context.Add(user);

            await _context.SaveChangesAsync();


            return user;
        }
    }
}
