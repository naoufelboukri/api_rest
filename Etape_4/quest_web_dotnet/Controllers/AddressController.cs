using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using quest_web.Models.Form;
using quest_web.Utils;
using System.Net.Http.Headers;

namespace quest_web.Controllers
{
    public class AddressController : Controller
    {
        private readonly APIDbContext _context;
        private readonly JwtTokenUtil _jwt;

        public AddressController(APIDbContext context, JwtTokenUtil JWT)
        {
            _context = context;
            _jwt = JWT;
        }

        [HttpGet("address")]
        [Authorize]
        public IActionResult addresses([FromHeader] string Authorization)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));

                if (user.Role == "ROLE_USER") {
                    var addresses = _context.address.Where(a => a.UserId == user.Id).ToList();
                    return Ok(addresses);
                } else if (user.Role == "ROLE_ADMIN")
                {
                    var addresses = _context.address.ToList();
                    return Ok(addresses);
                }
            }
            return StatusCode(403, new { message = "Accès non autorisé" });
        }

        [HttpGet("address/{id}")]
        [Authorize]
        public IActionResult address([FromHeader] string Authorization, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);

                var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));

                if (user.Role == "ROLE_USER")
                {
                    var addresses = _context.address.Where(a => a.UserId == user.Id).ToList();
                    var address = addresses.FirstOrDefault(adr => (adr.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(400, new { message = "Cette adresse n'existe pas" });
                    }
                    return Ok(address);
                } else if (user.Role == "ROLE_ADMIN")
                {
                    var address = _context.address.FirstOrDefault(adr => (adr.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(400, new { message = "Cette adresse n'existe pas ou ne vous appartient pas" });
                    }
                    return Ok(address);
                }

            }
            return StatusCode(403, new { message = "Accès non autorisé" });
        }


        [HttpPost("address")]
        [Authorize]
        public async Task<IActionResult> newAddress([FromBody] AddressBody request, [FromHeader] string Authorization)
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
                var address = new Address
                {
                    City = request.city,
                    Road = request.street,
                    PostalCode = request.postalCode,
                    Country = request.country,
                    UserId = user.Id
                };
                _context.address.Add(address);
                user.Addresses.Add(address);
                _context.SaveChanges();
                return Ok(address);
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }
        [HttpPut("address/{id}")]
        [Authorize]
        public async Task<IActionResult> putAddress([FromBody] AddressBody request, [FromHeader] string Authorization, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }
}
