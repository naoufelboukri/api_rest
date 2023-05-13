using quest_web;
using quest_web.Models;
using System.Net.Http.Headers;

namespace quest_web_dotnet.Services
{
    public class JwtService
    {
        private readonly JwtTokenUtil _jwt;
        private readonly APIDbContext _context;

        public JwtService(JwtTokenUtil jwt, APIDbContext context)
        {
            _jwt = jwt;
            _context = context;
        }

        public string? getUsername(string Autorization)
        {
            if (AuthenticationHeaderValue.TryParse(Autorization, out var headerValue))
            {
                return _jwt.GetUsernameFromToken(headerValue.Parameter);
            }
            return null;
        }

        public User? getUser(string Authorization)
        {
            var username = getUsername(Authorization);
            if (username != null)
            {
                var user = _context.users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
