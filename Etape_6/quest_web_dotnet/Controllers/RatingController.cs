using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quest_web;
using quest_web_dotnet.Models;
using quest_web_dotnet.Models.Forms;

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
            var result = await base.Create(Authorization, request);
            return result;
        }
    }
}
