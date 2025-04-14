using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok("Post Controller");
        }
    }
}
