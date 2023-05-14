using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quest_web;
using quest_web_dotnet.Models;
using quest_web_dotnet.Services;

namespace quest_web_dotnet.Controllers
{
    public class BaseController<T, F> : Controller where T : class where F : class
    {
        protected DbSet<T> _contextName;
        protected F _form;

        protected readonly APIDbContext _context;
        protected readonly JwtTokenUtil _jwt;


        public BaseController (APIDbContext context, JwtTokenUtil jwt, DbSet<T> contextName, F form)
        {            
            _context = context;
            _jwt = jwt;

            _contextName = contextName;
            _form = form;
        }

        public IActionResult getAll()
        {
            return Ok(_contextName.ToArray());
        }

        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            var entity = _contextName.Find(id);
            if (entity == null)
            {
                return BadRequest(new { message = "[" + _contextName + "] L'enregistrement " + id + "n'existe pas." });
            }
            return Ok(entity);
        }

        [HttpPost]
        [Authorize]
        public virtual async Task<IActionResult> Create([FromHeader] string Authorization, [FromBody] F request)
        {
            var user = new JwtService(_jwt, _context).getUser(Authorization);
            if (user != null)
            {
                var entity = _contextName.Add(request);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), entity);
            }
            return StatusCode(403, new { message = "Vous n'avez pas les droits" });
        }
    }
}
