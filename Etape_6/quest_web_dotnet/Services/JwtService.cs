using quest_web;
using quest_web.Models;
using System.Net.Http.Headers;

namespace quest_web_dotnet.Services
{
    public class JwtService
    {
        private readonly JwtTokenUtil _jwt;

        public JwtService(JwtTokenUtil jwt)
        {
            _jwt = jwt;
        }

        public string? getUsername(string Autorization)
        {
            if (AuthenticationHeaderValue.TryParse(Autorization, out var headerValue))
            {
                return _jwt.GetUsernameFromToken(headerValue.Parameter);
            }
            return null;
        }
    }
}
