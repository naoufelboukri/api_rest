using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Models;
using quest_web_dotnet.Services;
using System.Net.Http.Headers;
using static quest_web_dotnet.Controllers.PostController;

namespace quest_web_dotnet.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        protected DbSet<T> _contextName;
        protected readonly APIDbContext _context;
        protected readonly JwtTokenUtil _jwt;

        protected dynamic badRequestMessage = new { message = "Une erreur s'est produite" };
        protected dynamic unauthorizeMessage = new { message = "Vous n'avez pas les droits" };
        protected dynamic deleteMessage = new { message = "L'objet a bien été supprimé" };

        public BaseController (APIDbContext context, JwtTokenUtil jwt, DbSet<T> contextName)
        {            
            _context = context;
            _jwt = jwt;

            _contextName = contextName;
        }

        public virtual IActionResult getAll([FromQuery] PaginationParameters paginationParameters)
        {
            var entities = from s in _contextName select s;
            var pagedList = PagedList<T>.toPagedList(entities, paginationParameters.PageNumber, paginationParameters.PageSize);
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };
            return Ok(new { data = pagedList, meta = metadata });
        }

        [HttpGet("{id}")]
        public virtual IActionResult get(int id)
        {
            var entity = _contextName.Find(id);
            if (entity == null)
            {
                return BadRequest(errorMessageExist(id));
            }
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult delete([FromHeader] string Authorization, int id)
        {
            User? user = getUser(Authorization);
            T? entity = _contextName.Find(id);
            if (user != null)
            {
                if (entity != null)
                {
                    IEntity iEntity = null;
                    if (entity is IEntity)
                    {
                        iEntity = (IEntity)entity;
                    }
                    if (user.Role == "ROLE_ADMIN" || (iEntity != null && iEntity.UserId == user.Id))
                    {
                        _contextName.Remove(entity);
                        _context.SaveChanges();
                        return Ok(deleteMessage);
                    }
                    return StatusCode(403, unauthorizeMessage);
                }
                return BadRequest(badRequestMessage);
            }
            return StatusCode(403, unauthorizeMessage);
        }

        protected User? getUser(string Authorization)
        {
            if (AuthenticationHeaderValue.TryParse(Authorization, out var headerValue))
            {
                var username = _jwt.GetUsernameFromToken(headerValue.Parameter);
                if (username != null)
                {
                    User? user = _context.users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        return user;
                    }
                }
            }
            return null;
        }
        protected dynamic errorMessageExist(int id)
        {
            return new { message = "L'entité " + id + " n'existe pas" };
        }
    }
}
