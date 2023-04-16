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


                var user = new User(username, password);
                _context.Add(user);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(register), new UserDetails(username, user.Role));
            
        }
    }
}
