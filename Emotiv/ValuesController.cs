using Microsoft.AspNetCore.Mvc;

namespace Emotiv
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}
