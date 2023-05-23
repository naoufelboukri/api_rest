using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace quest_web_dotnet.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : BaseController<User>
    {
        public UserController(APIDbContext context, JwtTokenUtil jwt) : base(context, jwt, context.users) { }

        //[HttpGet]
        //public override IActionResult getAll(PaginationParameters paginationParameters)
        //{
        //    var users = from s in _contextName select s;
        //    var results = users.Include(u => u.Ratings).Include(u => u.Posts);
        //    var pagedList = PagedList<Post>.toPagedList(results, paginationParameters.PageNumber, paginationParameters.PageSize);
        //    var metadata = new
        //    {
        //        pagedList.TotalCount,
        //        pagedList.PageSize,
        //        pagedList.CurrentPage,
        //        pagedList.TotalPages,
        //        pagedList.HasNext,
        //        pagedList.HasPrevious
        //    };
        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        //    return Ok(pagedList);
        //}

        [HttpGet("{id}")]
        public override IActionResult get(int id)
        {
            var entity = _contextName.Find(id);
            _contextName.Include(u => u.Ratings).Include(u => u.Posts).ToList();
            if (entity == null)
            {
                return BadRequest(errorMessageExist(id));
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit([FromHeader] string Authorization, JsonObject request, int id)
        {
            User? connectedUser = getUser(Authorization);

            if (connectedUser != null && (connectedUser.Role == "ROLE_ADMIN" || connectedUser.Id == id))
            {
                User? targetUser = _contextName.Find(id);
                if (targetUser != null)
                {
                    targetUser.Username = (string)(request.ContainsKey("username") ? request["username"] : targetUser.Username);
                    targetUser.Role = (string)(request.ContainsKey("role") ? request["role"] : targetUser.Role);
                    targetUser.Updated_At = DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new UserDetails(targetUser.Username, targetUser.Role));
                }
                return BadRequest(errorMessageExist(id));
            }
            return StatusCode(403, unauthorizeMessage);
        }
    }
}
