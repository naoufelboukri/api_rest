using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web.Models;
using quest_web.Models.Form;
using quest_web.Utils;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

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

                if (user != null)
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

                if (user != null)
                {
                    var address = _context.address.FirstOrDefault(a => a.Id == int.Parse(id));
                    if (address == null)
                    {
                        return StatusCode(400, new { message = "Cette adresse n'existe pas" });
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
                return CreatedAtAction(nameof(newAddress), address);
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }

        [HttpPut("address/{id}")]
        [Authorize]
        public async Task<IActionResult> putAddress([FromHeader] string Authorization, [FromBody] JsonObject request, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
               var token = headerValue.Parameter;
               var username = _jwt.GetUsernameFromToken(token);
               var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));

               if (user.Role == "ROLE_USER")
               {
                    var address = _context.address.FirstOrDefault(address => (address.UserId == user.Id && address.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(403, new { message = "Cette adresse n'existe pas" });
                    }
                    address.Road = (string)(request.ContainsKey("street") ? request["street"] : address.Road);
                    address.City = (string)(request.ContainsKey("city") ? request["city"] : address.City);
                    address.PostalCode = (string)(request.ContainsKey("postalCode") ? request["postalCode"] : address.PostalCode);
                    address.Country = (string)(request.ContainsKey("country") ? request["country"] : address.Country);
                    address.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(address);
                    
               } else if (user.Role == "ROLE_ADMIN") {
                    var address = _context.address.FirstOrDefault(address => (address.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(400, new { message = "Cette adresse n'existe pas" });
                    }
                    address.Road = (string)(request.ContainsKey("street") ? request["street"] : address.Road);
                    address.City = (string)(request.ContainsKey("city") ? request["city"] : address.City);
                    address.PostalCode = (string)(request.ContainsKey("postalCode") ? request["postalCode"] : address.PostalCode);
                    address.Country = (string)(request.ContainsKey("country") ? request["country"] : address.Country);
                    address.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(address);
               }

            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }

        [HttpDelete("address/{id}")]
        [Authorize]
        public async Task<IActionResult> deleteAddress([FromHeader] string Authorization, string id)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var token = headerValue.Parameter;
                var username = _jwt.GetUsernameFromToken(token);
                var user = _context.user.ToList().FirstOrDefault(user => (user.Username == username));

                if (user.Role == "ROLE_USER")
                {
                    var address = _context.address.FirstOrDefault(address => (address.UserId == user.Id && address.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(403, new { success = "false" });
                    }
                    _context.Remove(address);
                    _context.SaveChanges();
                    return Ok(new { success = "true" });
                }
                else if (user.Role == "ROLE_ADMIN")
                {
                    var address = _context.address.FirstOrDefault(address => (address.Id == int.Parse(id)));
                    if (address == null)
                    {
                        return StatusCode(403, new { success = "false" });
                    }
                    _context.Remove(address);
                    _context.SaveChanges();
                    return Ok(new { success = "true" });
                }
            }
            return BadRequest(new { message = "Vous n'avez pas les droits" });
        }
    }
}
