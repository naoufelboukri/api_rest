using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("ratings")]
    public class RatingController : BaseController<Rating>
    {
        public RatingController(APIDbContext context, JwtTokenUtil jwt) : base (context, jwt, context.ratings) { }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromHeader] string Authorization, [FromBody] RatingBody request)
        {
            var user = getUser(Authorization);
            Post? post = _context.posts.Find(request.PostId);
            if (post == null)
            {
                return BadRequest(errorMessageExist(request.PostId));
            }
            if (user != null)
            {
                Rating rating = new Rating
                {
                    Content = request.Content,
                    Value = request.Value,
                    UserId = user.Id,
                    PostId = request.PostId
                };

                _context.ratings.Add(rating);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), rating);
            }
            return StatusCode(403, unauthorizeMessage);
        }
    }
}
