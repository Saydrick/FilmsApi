using Microsoft.AspNetCore.Mvc;

namespace FilmsApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { message = "API Films opérationnelle", version = "1.0" });
    }
}