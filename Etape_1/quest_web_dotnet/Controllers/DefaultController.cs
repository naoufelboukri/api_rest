using Microsoft.AspNetCore.Mvc;

namespace quest_web.Controllers;

[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet("testSuccess")]
    public IActionResult testSuccess()
    {
        return this.Ok("success");
    }

    [HttpGet("testNotFound")]
    public IActionResult testNotFound()
    {
        return this.NotFound("not found");
    }

    [HttpGet("testError")]
    public IActionResult testError()
    {
        return StatusCode(500, "error");
    }
}